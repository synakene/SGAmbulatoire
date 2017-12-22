using UnityEngine;
using System.Xml;
using System.Collections;

public class SendXmlTemp : MonoBehaviour {

	void Start()
	{
		StartCoroutine(SendData());
	}

	IEnumerator SendData()
	{
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load("text-xml.xml");
		WWWForm form = new WWWForm();
		form.AddField("xml", xmlDoc.InnerXml);
		WWW www = new WWW("http://www.banques.local/api/v1/sg2/data", form);

		while (!www.isDone)
		{
			print("Sending... " + www.progress);
			yield return null;
		}

		print(www.text);

		print("finished !");
	}
}
