using sharpPDF;
using sharpPDF.Enumerators;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class SimplePDF : MonoBehaviour {
	
	internal	string		attacName	= "Resultats.pdf";
    public string nomPDF;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return StartCoroutine(CreatePDF());
    }

    // Update is called once per frame
    public IEnumerator CreatePDF ()
	{
        CompleteFeedback feedback = Data.CompleteFeedback;

		XmlDocument xmlWriter = new XmlDocument();
		XmlNode root = xmlWriter.CreateElement("scoreSG");
		xmlWriter.AppendChild(root);

		WWW www = null;

		pdfDocument myDoc = new pdfDocument(nomPDF, "Synakene", false);

        pdfPage page = myDoc.addPage();
        List<pdfPage> pages = new List<pdfPage>();
        pages.Add(page);

        page.addText(Data.playerName, 16, 735, predefinedFont.csHelvetica, 14, new pdfColor(predefinedColor.csBlack));
        page.addText("L’accueil et la préparation du patient dans le cadre de chirurgie ambulatoire.", 16, 710, predefinedFont.csHelvetica, 16, new pdfColor(predefinedColor.csRaspberry));

        page.addText("Être centré sur le patient : " + Data.curScoreObj1 + "/" + Data.MaxScoreObj1, 26, 685, predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csBlack));
        page.addText("Utiliser le raisonnement clinique : " + Data.curScoreObj2 + "/" + Data.MaxScoreObj2, 26, 670, predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csBlack));
        page.addText("Respecter la procédure : " + Data.curScoreObj3 + "/" + Data.MaxScoreObj3, 26, 655, predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csBlack));


        float score = ((Data.curScoreObj1 + Data.curScoreObj2 + Data.curScoreObj3) / Data.MaxTotal) * 100f;
        int scoreTotal = Mathf.FloorToInt(score);

        page.addText("Score Total : " + scoreTotal + "%", 26, 630, predefinedFont.csHelveticaBold, 14, new pdfColor(predefinedColor.csRaspberry));


        pdfTable myTable = new pdfTable();
        pdfTable SuccessTable = new pdfTable();
        /*Add Columns to a grid*/
        SuccessTable.tableHeader.addColumn(new pdfTableColumn("", predefinedAlignment.csLeft, 6));

        myTable.tableHeader.addColumn(new pdfTableColumn("Contexte", predefinedAlignment.csLeft, 195));
        myTable.tableHeader.addColumn(new pdfTableColumn("Réponses", predefinedAlignment.csLeft, 195));
        myTable.tableHeader.addColumn(new pdfTableColumn("Conseil", predefinedAlignment.csLeft, 195));

		// XML Generation
		{
			XmlNode user = CreateAndAppendChild(xmlWriter, root, "user");
			CreateAndAppendChild(xmlWriter, user, "id", Data.learnerId);
			CreateAndAppendChild(xmlWriter, user, "lastname", Data.learnerName);
			CreateAndAppendChild(xmlWriter, user, "firstname", Data.learnerName);

			string apiPath = Application.streamingAssetsPath + "/key.txt";
			string apiKey;

			if (apiPath.Contains("://"))
			{
				WWW wwww = new WWW(apiPath);
				yield return wwww;
				apiKey = wwww.text;
			}
			else
				apiKey = File.ReadAllText(apiPath);

			CreateAndAppendChild(xmlWriter, root, "APIkey", apiKey);

			#region XML Scores
			XmlNode scores = CreateAndAppendChild(xmlWriter, root, "scores");

			XmlNode scoreXml = CreateAndAppendChild(xmlWriter, scores, "score");
			CreateAndAppendChild(xmlWriter, scoreXml, "name", "obj1");
			CreateAndAppendChild(xmlWriter, scoreXml, "value", Data.curScoreObj1.ToString());
			CreateAndAppendChild(xmlWriter, scoreXml, "maxValue", Data.MaxScoreObj1.ToString());

			scoreXml = CreateAndAppendChild(xmlWriter, scores, "score");
			CreateAndAppendChild(xmlWriter, scoreXml, "name", "obj2");
			CreateAndAppendChild(xmlWriter, scoreXml, "value", Data.curScoreObj2.ToString());
			CreateAndAppendChild(xmlWriter, scoreXml, "maxValue", Data.MaxScoreObj2.ToString());

			scoreXml = CreateAndAppendChild(xmlWriter, scores, "score");
			CreateAndAppendChild(xmlWriter, scoreXml, "name", "obj3");
			CreateAndAppendChild(xmlWriter, scoreXml, "value", Data.curScoreObj3.ToString());
			CreateAndAppendChild(xmlWriter, scoreXml, "maxValue", Data.MaxScoreObj3.ToString());

			scoreXml = CreateAndAppendChild(xmlWriter, scores, "score");
			CreateAndAppendChild(xmlWriter, scoreXml, "name", "obj4");
			CreateAndAppendChild(xmlWriter, scoreXml, "value", "0");
			CreateAndAppendChild(xmlWriter, scoreXml, "maxValue", "0");
			#endregion

			CreateAndAppendChild(xmlWriter, root, "time", (Data.min * 60 + Data.sec).ToString());

			CreateAndAppendChild(xmlWriter, root, "totalScore", scoreTotal.ToString());
		}

		if (feedback != null)
        {
			XmlNode questions = CreateAndAppendChild(xmlWriter, root, "data");

			foreach (CompleteFeedback.Info info in feedback.CompleteFeedbackList)
            {
				string question;
				string answer = "";
				string goodAnswer = "";
				string feedbackInfo = "";

				if (info.Feedback != "RESTART")
                {
                    pdfTableRow myRow = myTable.createRow();
                    pdfTableRow SuccessRow = SuccessTable.createRow();

                    myRow[0].columnValue = info.Question + "\n\n" + "Que répondez-vous ?";
					question = info.Question;
					goodAnswer = info.GoodAnswer;

                    if (info.IdChoice == info.IdGoodAnswer)
                    {
                        myRow[1].columnValue = "Réponse effectuée : " + info.GoodAnswer;
						answer = info.GoodAnswer;
                        SuccessRow.RowStyleProp = new pdfTableRowStyle(predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csBlack), new pdfColor(predefinedColor.csMyGreen), new pdfColor(predefinedColor.csBlack));
                    }
                    else
                    {
                        foreach (KeyValuePair<int, string> entry in info.Reponses)
                        {
                            if (entry.Key == info.IdChoice)
                            {
                                myRow[1].columnValue = "Réponse effectuée : " + entry.Value;
								answer = entry.Value;
                            }
                        }
                        myRow[1].columnValue += "\n\n" + "Réponse attendue : " + info.GoodAnswer;
                        SuccessRow.RowStyleProp = new pdfTableRowStyle(predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csBlack), new pdfColor(predefinedColor.csMyRed), new pdfColor(predefinedColor.csBlack));
                    }
                    myRow[2].columnValue = feedbackInfo = info.Feedback;

                    myTable.addRow(myRow);
                    SuccessTable.addRow(SuccessRow);
                }
                else
                {
                    pdfTableRow myRow = myTable.createRow(3);
                    pdfTableRow SuccessRow = SuccessTable.createRow();

                    myRow[0].columnValue = question = "Situation critique, retour au début de l'activité ";

                    if (info.Question == Data.S1)
                    {
                        myRow[0].columnValue += "\"" + Data.S1 + "\"";
                    }
                    if (info.Question == Data.S2)
                    {
                        myRow[0].columnValue += "\"" + Data.S2 + "\"";
                    }
                    if (info.Question == Data.S3)
                    {
                        myRow[0].columnValue += "\"" + Data.S3 + "\"";
                    }
                    if (info.Question == Data.S4)
                    {
                        myRow[0].columnValue += "\"" + Data.S4 + "\"";
                    }
                    if (info.Question == Data.S5)
                    {
                        myRow[0].columnValue += "\"" + Data.S5 + "\"";
                    }
                    SuccessRow.RowStyleProp = new pdfTableRowStyle(predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csWhite), new pdfColor(predefinedColor.csWhite), new pdfColor(predefinedColor.csBlack));

                    myTable.addRow(myRow);
                    SuccessTable.addRow(SuccessRow);

					question = myRow[0].columnValue;
                }

				#region XML Question

				Dictionary<string, string> dict = new Dictionary<string, string>();
				dict.Add("valid", (info.Feedback != "RESTART" && answer.Equals(goodAnswer) ? "true" : "false"));
				XmlNode questionXml = CreateAndAppendChild(xmlWriter, questions, "question", attributes: dict);

				CreateAndAppendChild(xmlWriter, questionXml, "asked", question);
				CreateAndAppendChild(xmlWriter, questionXml, "answer", answer);
				CreateAndAppendChild(xmlWriter, questionXml, "goodAnswer", goodAnswer);
				CreateAndAppendChild(xmlWriter, questionXml, "feedback", feedbackInfo);

				#endregion
			}

			{
				#region API Key fetching 
				string filePath = Application.streamingAssetsPath + "/server.txt";
				string url;

				if (filePath.Contains("://"))
				{
					WWW wwww = new WWW(filePath);
					yield return wwww;
					url = wwww.text;
				}
				else
					url = File.ReadAllText(filePath);

				#endregion

				#region XMLwriting
				xmlWriter.Save("text-xml.xml");

				WWWForm form = new WWWForm();
				form.AddField("xml", xmlWriter.InnerXml);
				WWW ws = new WWW(url, form);

				while (ws.isDone == false)
				{
					Debug.Log("Sending..." + ws.progress);
					yield return null;
				}
				#endregion
			}
		}

        /*Set Header's Style*/
        myTable.tableHeaderStyle = new pdfTableRowStyle(predefinedFont.csHelveticaBold, 12, new pdfColor(predefinedColor.csWhite), new pdfColor(predefinedColor.csRaspberry), new pdfColor(predefinedColor.csBlack));
        /*Set Row's Style*/
        myTable.rowStyle = new pdfTableRowStyle(predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csBlack), new pdfColor(predefinedColor.csWhite), new pdfColor(predefinedColor.csBlack));
        /*Set Alternate Row's Style*/
        myTable.alternateRowStyle = new pdfTableRowStyle(predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csBlack), new pdfColor(predefinedColor.csWhite), new pdfColor(predefinedColor.csBlack));
        /*Set Cellpadding*/
        myTable.cellpadding = 5;

        page.addTable(myDoc, pages, myTable, 16, 610, 680);
        page.addSuccessTable(myDoc, pages, SuccessTable, 8, 610, 680, myTable);

        /* Add header to each pages */
        for (int i = 1; i < pages.Count; i++)
        {
            pages[i].addText(Data.playerName, 16, 735, predefinedFont.csHelvetica, 14, new pdfColor(predefinedColor.csBlack));
            pages[i].addText("L’accueil et la préparation du patient dans le cadre de chirurgie ambulatoire.", 16, 710, predefinedFont.csHelvetica, 16, new pdfColor(predefinedColor.csRaspberry));
            pdfTable t = new pdfTable();
            /*Add Columns to a grid*/
            t.tableHeader.addColumn(new pdfTableColumn("Contexte", predefinedAlignment.csLeft, 195));
            t.tableHeader.addColumn(new pdfTableColumn("Reponses", predefinedAlignment.csLeft, 195));
            t.tableHeader.addColumn(new pdfTableColumn("Conseil", predefinedAlignment.csLeft, 195));

            /*Set Header's Style*/
            t.tableHeaderStyle = new pdfTableRowStyle(predefinedFont.csHelveticaBold, 12, new pdfColor(predefinedColor.csWhite), new pdfColor(predefinedColor.csRaspberry), new pdfColor(predefinedColor.csBlack));
            /*Set Row's Style*/
            t.rowStyle = new pdfTableRowStyle(predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csBlack), new pdfColor(predefinedColor.csWhite), new pdfColor(predefinedColor.csBlack));
            /*Set Alternate Row's Style*/
            t.alternateRowStyle = new pdfTableRowStyle(predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csBlack), new pdfColor(predefinedColor.csWhite), new pdfColor(predefinedColor.csBlack));
            /*Set Cellpadding*/
            t.cellpadding = 5;

            pages[i].addHeader(t, 16, 680);
        }

        yield return new WaitForSeconds(0);

        if (Application.isWebPlayer || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            List<string> data = new List<string>();
            data = myDoc.CustumCreatePDF();

            string res = "";
            foreach (string s in data)
            {
                res += s;
            }
            Application.ExternalCall("namePDF", Data.playerName);
            Application.ExternalCall("PDF", res);
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            string fileName = nomPDF + "-" + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + ".pdf";
            myDoc.createPDF(Application.persistentDataPath + "/" + fileName);
            string path = Application.persistentDataPath + "/" + fileName;
            Application.OpenURL(path);
        }
        else
        {
            string fileName = nomPDF + "-" + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + ".pdf";
            myDoc.createPDF(fileName);
        }

    }

	private XmlNode CreateAndAppendChild(XmlDocument writer, XmlNode parent, string nodeName, string innerText = "", Dictionary<string, string> attributes = null)
	{
		XmlNode node = writer.CreateElement(nodeName);

		if (innerText != "")
			node.InnerText = innerText;

		if (attributes != null)
		{
			foreach(KeyValuePair<string, string> attribute in attributes)
			{
				XmlAttribute attr = writer.CreateAttribute(attribute.Key);
				attr.Value = attribute.Value;
				node.Attributes.Append(attr);
			}
		}

		parent.AppendChild(node);

		return node;
	}
}
