using UnityEngine;
using System.Collections;
using System.IO;
using sharpPDF;
using sharpPDF.Enumerators;
using System.Collections.Generic;

public class SimplePDF : MonoBehaviour {
	
	internal	string		attacName	= "Resultats.pdf";
    public string nomPDF;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return StartCoroutine(CreatePDF());
    }

    // Update is called once per frame
    public IEnumerator CreatePDF () {
        
        CompleteFeedback feedback = Data.CompleteFeedback;

        pdfDocument myDoc = new pdfDocument(nomPDF, "Synakene", false);

        pdfPage page = myDoc.addPage();
        List<pdfPage> pages = new List<pdfPage>();
        pages.Add(page);

        page.addText(Data.playerName, 16, 735, predefinedFont.csHelvetica, 14, new pdfColor(predefinedColor.csBlack));
        page.addText("L’accueil et la préparation du patient dans le cadre de chirurgie ambulatoire.", 16, 710, predefinedFont.csHelvetica, 16, new pdfColor(predefinedColor.csRaspberry));

        page.addText("Être centré sur le patient : " + Data.scoreObj1 + "/" + Data.MaxScoreObj1, 26, 685, predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csBlack));
        page.addText("Utiliser le raisonnement clinique : " + Data.scoreObj2 + "/" + Data.MaxScoreObj2, 26, 670, predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csBlack));


        float score = ((Data.scoreObj1 + Data.scoreObj2) / Data.MaxTotal) * 100f;
        int scoreTotal = Mathf.FloorToInt(score);

        page.addText("Score Total : " + scoreTotal + "%", 26, 645, predefinedFont.csHelveticaBold, 14, new pdfColor(predefinedColor.csRaspberry));


        pdfTable myTable = new pdfTable();
        pdfTable SuccessTable = new pdfTable();
        /*Add Columns to a grid*/
        SuccessTable.tableHeader.addColumn(new pdfTableColumn("", predefinedAlignment.csLeft, 6));

        myTable.tableHeader.addColumn(new pdfTableColumn("Contexte", predefinedAlignment.csLeft, 195));
        myTable.tableHeader.addColumn(new pdfTableColumn("Réponses", predefinedAlignment.csLeft, 195));
        myTable.tableHeader.addColumn(new pdfTableColumn("Conseil", predefinedAlignment.csLeft, 195));

        if (feedback != null)
        {
            foreach (CompleteFeedback.Info info in feedback.CompleteFeedbackList)
            {
                pdfTableRow myRow = myTable.createRow();
                pdfTableRow SuccessRow = SuccessTable.createRow();

                myRow[0].columnValue = info.Question + "\n\n" + "Que répondez-vous ?";
                if (info.IdChoice == info.IdGoodAnswer)
                {
                    myRow[1].columnValue = "Réponse effectuée : " + info.GoodAnswer;
                    SuccessRow.RowStyleProp = new pdfTableRowStyle(predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csBlack), new pdfColor(predefinedColor.csMyGreen), new pdfColor(predefinedColor.csBlack));
                } else
                {
                    foreach (KeyValuePair<int, string> entry in info.Reponses)
                    {
                        if (entry.Key == info.IdChoice)
                        {
                            myRow[1].columnValue = "Réponse effectuée : " + entry.Value;
                        }
                    }
                    myRow[1].columnValue += "\n\n" + "Réponse attendue : " + info.GoodAnswer;
                    SuccessRow.RowStyleProp = new pdfTableRowStyle(predefinedFont.csHelvetica, 10, new pdfColor(predefinedColor.csBlack), new pdfColor(predefinedColor.csMyRed), new pdfColor(predefinedColor.csBlack));
                }
                myRow[2].columnValue = info.Feedback;

                myTable.addRow(myRow);
                SuccessTable.addRow(SuccessRow);
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

        page.addTable(myDoc, pages, myTable, 16, 625, 680);
        page.addSuccessTable(myDoc, pages, SuccessTable, 8, 625, 680, myTable);

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

            Application.ExternalCall("PDF", res);

        }
        else
        {
            myDoc.createPDF(nomPDF + "-" + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString() + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + ".pdf");
        }

    }
}
