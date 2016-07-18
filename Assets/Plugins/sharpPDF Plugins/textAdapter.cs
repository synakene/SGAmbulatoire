using System;
using System.Collections;
using System.Text;

using sharpPDF.Exceptions;
using sharpPDF.Enumerators;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace sharpPDF
{
	/// <summary>
	/// Abstract class that implements different functions used for text and paragraph
	/// </summary>
	internal abstract class textAdapter
	{

		/// <summary>
		/// Static property with the dimensions of the standard fonts
		/// </summary>
		private static int[][] fontWeight = new int[12][] 
		{
			//Helvetica			
			new int[]{278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,355,556,556,889,667,191,333,333,389,584,278,333,278,278,556,556,556,556,556,556,556,556,556,556,278,278,584,584,584,556,1015,667,667,722,722,667,611,778,722,278,500,667,556,833,722,778,667,778,722,667,611,722,667,944,667,667,611,278,278,278,469,556,333,556,556,500,556,556,278,556,556,222,222,500,222,833,556,556,556,556,333,500,278,556,500,722,500,500,500,334,260,334,584,350,556,350,222,556,333,1000,556,556,333,1000,667,333,1000,350,611,350,350,222,222,333,333,350,556,1000,333,1000,500,333,944,350,500,667,278,333,556,556,556,556,260,556,333,737,370,556,584,333,737,333,400,584,333,333,333,556,537,278,333,333,365,556,834,834,834,611,667,667,667,667,667,667,1000,722,667,667,667,667,278,278,278,278,722,722,778,778,778,778,778,584,778,722,722,722,722,667,667,611,556,556,556,556,556,556,889,500,556,556,556,556,278,278,278,278,556,556,556,556,556,556,556,584,611,556,556,556,556,500,556,500},
			//HelveticaBold
			new int[]{278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,333,474,556,556,889,722,238,333,333,389,584,278,333,278,278,556,556,556,556,556,556,556,556,556,556,333,333,584,584,584,611,975,722,722,722,722,667,611,778,722,278,556,722,611,833,722,778,667,778,722,667,611,722,667,944,667,667,611,333,278,333,584,556,333,556,611,556,611,556,333,611,611,278,278,556,278,889,611,611,611,611,389,556,333,611,556,778,556,556,500,389,280,389,584,350,556,350,278,556,500,1000,556,556,333,1000,667,333,1000,350,611,350,350,278,278,500,500,350,556,1000,333,1000,556,333,944,350,500,667,278,333,556,556,556,556,280,556,333,737,370,556,584,333,737,333,400,584,333,333,333,611,556,278,333,333,365,556,834,834,834,611,722,722,722,722,722,722,1000,722,667,667,667,667,278,278,278,278,722,722,778,778,778,778,778,584,778,722,722,722,722,667,667,611,556,556,556,556,556,556,889,556,556,556,556,556,278,278,278,278,611,611,611,611,611,611,611,584,611,611,611,611,611,556,611,556},
			//csHelveticaOblique
			new int[]{278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,355,556,556,889,667,191,333,333,389,584,278,333,278,278,556,556,556,556,556,556,556,556,556,556,278,278,584,584,584,556,1015,667,667,722,722,667,611,778,722,278,500,667,556,833,722,778,667,778,722,667,611,722,667,944,667,667,611,278,278,278,469,556,333,556,556,500,556,556,278,556,556,222,222,500,222,833,556,556,556,556,333,500,278,556,500,722,500,500,500,334,260,334,584,350,556,350,222,556,333,1000,556,556,333,1000,667,333,1000,350,611,350,350,222,222,333,333,350,556,1000,333,1000,500,333,944,350,500,667,278,333,556,556,556,556,260,556,333,737,370,556,584,333,737,333,400,584,333,333,333,556,537,278,333,333,365,556,834,834,834,611,667,667,667,667,667,667,1000,722,667,667,667,667,278,278,278,278,722,722,778,778,778,778,778,584,778,722,722,722,722,667,667,611,556,556,556,556,556,556,889,500,556,556,556,556,278,278,278,278,556,556,556,556,556,556,556,584,611,556,556,556,556,500,556,500},
			//csHelvetivaBoldOblique
			new int[]{278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,278,333,474,556,556,889,722,238,333,333,389,584,278,333,278,278,556,556,556,556,556,556,556,556,556,556,333,333,584,584,584,611,975,722,722,722,722,667,611,778,722,278,556,722,611,833,722,778,667,778,722,667,611,722,667,944,667,667,611,333,278,333,584,556,333,556,611,556,611,556,333,611,611,278,278,556,278,889,611,611,611,611,389,556,333,611,556,778,556,556,500,389,280,389,584,350,556,350,278,556,500,1000,556,556,333,1000,667,333,1000,350,611,350,350,278,278,500,500,350,556,1000,333,1000,556,333,944,350,500,667,278,333,556,556,556,556,280,556,333,737,370,556,584,333,737,333,400,584,333,333,333,611,556,278,333,333,365,556,834,834,834,611,722,722,722,722,722,722,1000,722,667,667,667,667,278,278,278,278,722,722,778,778,778,778,778,584,778,722,722,722,722,667,667,611,556,556,556,556,556,556,889,556,556,556,556,556,278,278,278,278,611,611,611,611,611,611,611,584,611,611,611,611,611,556,611,556},
			//Courier			
			new int[]{600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600},
			//csCourierBold
			new int[]{600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600},
			//csCourierOblique
			new int[]{600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600},
			//csCourierBoldOblique
			new int[]{600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600,600},
			//csTimes
			new int[]{250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,333,408,500,500,833,778,180,333,333,500,564,250,333,250,278,500,500,500,500,500,500,500,500,500,500,278,278,564,564,564,444,921,722,667,667,722,611,556,722,722,333,389,722,611,889,722,722,556,722,667,556,611,722,722,944,722,722,611,333,278,333,469,500,333,444,500,444,500,444,333,500,500,278,278,500,278,778,500,500,500,500,333,389,278,500,500,722,500,500,444,480,200,480,541,350,500,350,333,500,444,1000,500,500,333,1000,556,333,889,350,611,350,350,333,333,444,444,350,500,1000,333,980,389,333,722,350,444,722,250,333,500,500,500,500,200,500,333,760,276,500,564,333,760,333,400,564,300,300,333,500,453,250,333,300,310,500,750,750,750,444,722,722,722,722,722,722,889,667,611,611,611,611,333,333,333,333,722,722,722,722,722,722,722,564,722,722,722,722,722,722,556,500,444,444,444,444,444,444,667,444,444,444,444,444,278,278,278,278,500,500,500,500,500,500,500,564,500,500,500,500,500,500,500,500},
			//csTimesBold
			new int[]{250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,333,555,500,500,1000,833,278,333,333,500,570,250,333,250,278,500,500,500,500,500,500,500,500,500,500,333,333,570,570,570,500,930,722,667,722,722,667,611,778,778,389,500,778,667,944,722,778,611,778,722,556,667,722,722,1000,722,722,667,333,278,333,581,500,333,500,556,444,556,444,333,500,556,278,333,556,278,833,556,500,556,556,444,389,333,556,500,722,500,500,444,394,220,394,520,350,500,350,333,500,500,1000,500,500,333,1000,556,333,1000,350,667,350,350,333,333,500,500,350,500,1000,333,1000,389,333,722,350,444,722,250,333,500,500,500,500,220,500,333,747,300,500,570,333,747,333,400,570,300,300,333,556,540,250,333,300,330,500,750,750,750,500,722,722,722,722,722,722,1000,722,667,667,667,667,389,389,389,389,722,722,778,778,778,778,778,570,778,722,722,722,722,722,611,556,500,500,500,500,500,500,722,444,444,444,444,444,278,278,278,278,500,556,500,500,500,500,500,570,500,556,556,556,556,500,556,500},
			//csTimesOblique
			new int[]{250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,333,420,500,500,833,778,214,333,333,500,675,250,333,250,278,500,500,500,500,500,500,500,500,500,500,333,333,675,675,675,500,920,611,611,667,722,611,611,722,722,333,444,667,556,833,667,722,611,722,611,500,556,722,611,833,611,556,556,389,278,389,422,500,333,500,500,444,500,444,278,500,500,278,278,444,278,722,500,500,500,500,389,389,278,500,444,667,444,444,389,400,275,400,541,350,500,350,333,500,556,889,500,500,333,1000,500,333,944,350,556,350,350,333,333,556,556,350,500,889,333,980,389,333,667,350,389,556,250,389,500,500,500,500,275,500,333,760,276,500,675,333,760,333,400,675,300,300,333,500,523,250,333,300,310,500,750,750,750,500,611,611,611,611,611,611,889,667,611,611,611,611,333,333,333,333,722,667,722,722,722,722,722,675,722,722,722,722,722,556,611,500,500,500,500,500,500,500,667,444,444,444,444,444,278,278,278,278,500,500,500,500,500,500,500,675,500,500,500,500,500,444,500,444},
			//csTimesBoldOblique
			new int[]{250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,250,389,555,500,500,833,778,278,333,333,500,570,250,333,250,278,500,500,500,500,500,500,500,500,500,500,333,333,570,570,570,500,832,667,667,667,722,667,667,722,778,389,500,667,611,889,722,722,611,722,667,556,611,722,667,889,667,611,611,333,278,333,570,500,333,500,500,444,500,444,333,500,556,278,278,500,278,778,556,500,500,500,389,389,278,556,444,667,500,444,389,348,220,348,570,350,500,350,333,500,500,1000,500,500,333,1000,556,333,944,350,611,350,350,333,333,500,500,350,500,1000,333,1000,389,333,722,350,389,611,250,389,500,500,500,500,220,500,333,747,266,500,606,333,747,333,400,570,300,300,333,576,500,250,333,300,300,500,750,750,750,500,667,667,667,667,667,667,944,667,667,667,667,667,389,389,389,389,722,722,722,722,722,722,722,570,722,722,722,722,722,611,611,500,500,500,500,500,500,500,722,444,444,444,444,444,278,278,278,278,500,556,500,500,500,500,500,570,500,556,556,556,556,444,500,444}
		};

        /// <summary>
        /// Static method that encodes the string
        /// </summary>
        /// <param name="strText">Input Text</param>
        /// <returns>Result of the encoding</returns>
        public static string encodeHEX(string strText)
        {
            char[] arrChar = strText.ToCharArray();
            string hexFormattedString = "";
            string appoStr;
            for (int i = 0; i < arrChar.Length; i++)
            {
                if ((arrChar[i] >= 0) && (arrChar[i] <= 255))
                {
                    appoStr = (Convert.ToByte(arrChar[i])).ToString("X2");
                    hexFormattedString += appoStr;
                }
                else
                {
                    appoStr = 27.ToString(); // Exa code of apostrophe
                    hexFormattedString += appoStr;
                }
            }
            return hexFormattedString;
        }

        //OLD CODE
        ///// <summary>
        ///// Static method that encodes the string
        ///// </summary>
        ///// <param name="strText">Input Text</param>
        ///// <returns>Result of the encoding</returns>
        //public static string encodeHEX(string strText)
        //{			
        //	char[] arrChar = strText.ToCharArray();
        //	string hexFormattedString = "";
        //	string appoStr;
        //	for (int i = 0; i <arrChar.Length ; i++)
        //	{
        //		if ((arrChar[i] >= 0) && (arrChar[i] <= 255)) {
        //			appoStr = (Convert.ToByte(arrChar[i])).ToString("X");
        //			if (appoStr.Length < 2) 
        //			{
        //				appoStr = "0" + appoStr;
        //			}
        //			hexFormattedString += appoStr;
        //		}
        //	}			
        //	return hexFormattedString;
        //}

        /// <summary>
        /// Static method that checks special characters into a string
        /// </summary>
        /// <param name="strText">Input Text</param>
        /// <returns>Formatted Text</returns>
        public static string checkText(string strText)
		{
			string checkedString = strText;
			checkedString = checkedString.Replace(@"\",@"\\");
			checkedString = checkedString.Replace("(",@"\(");
			checkedString = checkedString.Replace(")",@"\)");			
			return checkedString;
		}

		/// <summary>
		/// Static method thats format a paragraph
		/// </summary>
		/// <param name="strText">Input Text</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="fontType">Font's type</param>
		/// <param name="parWidth">Paragrapfh's width</param>
		/// <returns>IEnumerable interface that cointains paragraphLine objects</returns>
		public static IEnumerable formatParagraph(string strText,int fontSize,predefinedFont fontType,int parWidth)
		{
			return formatParagraph(strText,fontSize,fontType,parWidth,fontSize + 4,predefinedAlignment.csLeft);	
		}
		
		/// <summary>
		/// Static method thats format a paragraph
		/// </summary>
		/// <param name="strText">Input Text</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="fontType">Font's type</param>
		/// <param name="parWidth">Paragrapfh's width</param>
		/// <param name="lineHeight">Line's height</param>
		/// <returns>IEnumerable interface that cointains paragraphLine objects</returns>
		public static IEnumerable formatParagraph(string strText,int fontSize,predefinedFont fontType,int parWidth,int lineHeight)
		{
			return formatParagraph(strText,fontSize,fontType,parWidth,lineHeight,predefinedAlignment.csLeft);	
		}

		/// <summary>
		/// Static method thats format a paragraph
		/// </summary>
		/// <param name="strText">Input Text</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="fontType">Font's type</param>
		/// <param name="parWidth">Paragrapfh's width</param>
		/// <param name="lineHeight">Line's height</param>
		/// <param name="parAlign">Paragraph's Alignment</param>
		/// <returns>IEnumerable interface that cointains paragraphLine objects</returns>
		public static IEnumerable formatParagraph(string strText,int fontSize,predefinedFont fontType,int parWidth,int lineHeight,predefinedAlignment parAlign)
		{			
			string[] paragraphsArray = strText.Split("\n".ToCharArray());
			string[] bufferArray;
			int lineLength;
			paragraphLine tempPar;
			StringBuilder lineString = new StringBuilder(parWidth);	
			ArrayList resultArray = new ArrayList();
			lineLength = 0;
			foreach(string paragraph in paragraphsArray) {
				bufferArray = paragraph.Split(" ".ToCharArray());
				foreach(string word in bufferArray) {
					if ((textAdapter.wordWeight(word + " ",fontSize,fontType) + lineLength) > parWidth) {						
						switch (parAlign) {
							case predefinedAlignment.csLeft: default:
								tempPar = new paragraphLine(lineString.ToString(0,lineString.Length - 1),lineHeight,0);
								break;
							case predefinedAlignment.csRight:
								tempPar = new paragraphLine(lineString.ToString(0,lineString.Length - 1),lineHeight,parWidth - lineLength);
								break;
							case predefinedAlignment.csCenter:
								tempPar = new paragraphLine(lineString.ToString(0,lineString.Length - 1),lineHeight,Convert.ToInt32((parWidth - lineLength) / 2));
								break;
						}
						resultArray.Add(tempPar);
						lineString.Remove(0,lineString.Length);
						lineLength = 0;
					}
					lineString.Append(word + " ");
					lineLength += textAdapter.wordWeight(word + " ",fontSize,fontType);
				}
				if (lineLength > 0) {
					switch (parAlign) {
						case predefinedAlignment.csLeft: default:
							tempPar = new paragraphLine(lineString.ToString(0,lineString.Length - 1),lineHeight,0);
							break;
						case predefinedAlignment.csRight:
							tempPar = new paragraphLine(lineString.ToString(0,lineString.Length - 1),lineHeight,parWidth - lineLength);
							break;
						case predefinedAlignment.csCenter:
							tempPar = new paragraphLine(lineString.ToString(0,lineString.Length - 1),lineHeight,Convert.ToInt32((parWidth - lineLength) / 2));
							break;
					}
					resultArray.Add(tempPar);
					lineString.Remove(0,lineString.Length);
					lineLength = 0;
				}
				bufferArray = null;
			}
			return resultArray;	
		}

		/// <summary>
		/// Static Method that returns the lenght of a single word
		/// </summary>
		/// <param name="word">Input word</param>
		/// <param name="fontSize">Font's size</param>
		/// <param name="fontType">Font's type</param>
		/// <returns>Size of the word</returns>
		public static int wordWeight(string word,int fontSize,predefinedFont fontType)
		{			
			double returnWeight = 0;
			foreach(char myChar in word.ToCharArray()) {
				if ((myChar >= 0) && (myChar <= 255)) {
					returnWeight += textAdapter.fontWeight[Convert.ToInt32(fontType) - 1][Convert.ToByte(myChar)];
				}
			}
			return Convert.ToInt32(returnWeight * fontSize /1000);
		}

        /// <summary>
        /// Static Method that returns a word list from a string with separators
        /// </summary>
        /// <param name="sentence">Input string</param>
        /// <param name="fontSize">Font's size</param>
        /// <param name="fontType">Font's type</param>
        /// <returns>List of words</returns>
        private static List<string> getWords(string sentence, int fontSize, predefinedFont fontType)
        {
            List<string> wordList = new List<string>();
            if (sentence != null)
            {
                string[] strings = Regex.Split(sentence, @"(\s|\n)");
                foreach (string s in strings)
                {
                    if (!Regex.IsMatch(s, @"\[(.*?)\]")) //Don't take [f] or [pic=2]
                        wordList.Add(s);
                }
            }
            return wordList;
        }

        /// <summary>
        /// Static Method that returns a list of strings with the length of a cell
        /// </summary>
        /// <param name="sentence">Input string</param>
        /// <param name="textSpace">Cell's length</param>
        /// <param name="fontSize">Font's size</param>
        /// <param name="fontType">Font's type</param>
        /// <param name="cellpadding">Table's cell padding</param>
        /// <returns>Size of the word</returns>
        public static List<string> getTextList(string sentence, int textSpace, int fontSize, predefinedFont fontType, int cellpadding)
        {
            List<string> sentenceList = new List<string>();
            List<string> words = new List<string>();
            words = getWords(sentence, fontSize, fontType);

            int index = 0;
            sentenceList.Add("");
            foreach (string word in words)
            {
                if (wordWeight(word, fontSize, fontType) + wordWeight(sentenceList[index], fontSize, fontType) >= textSpace || word == "\n")
                {
                    index++;
                    sentenceList.Add("");
                }
                if (wordWeight(word, fontSize, fontType) + wordWeight(sentenceList[index], fontSize, fontType) + cellpadding < textSpace)
                {
                    if (!(sentenceList[index].Length == 0) || !(word == " " || word == "\n"))
                        sentenceList[index] += word;
                } else
                {
                    if (word != " " && word != "\n")
                    {
                        index++;
                        sentenceList.Add(word);
                    }
                }
            }
            return sentenceList;
        }

        /// <summary>
        /// Static Method that crop a word to put it in a predefined space
        /// </summary>
        /// <param name="word">Input word</param>
        /// <param name="fontSize">Font's size</param>
        /// <param name="fontType">Font's type</param>
        /// <param name="textSpace">Max text's space</param>
        /// <returns>Cropped word</returns>
        public static string cropWord(string word, int fontSize, predefinedFont fontType, int textSpace)
        {
            StringBuilder tempWord = new StringBuilder();
            int i = 0;
            while ((wordWeight(tempWord.ToString(), fontSize, fontType) <= textSpace) && (i < word.Length))
            {
                if ((word[i] >= 0) && (word[i] <= 255))
                {
                    tempWord.Append(word.ToCharArray()[i]);
                }
                i++;
            }
            if (tempWord.ToString().CompareTo(word) != 0)
            {
                tempWord.Remove(tempWord.Length - 3, 3);
                tempWord.Append("...");
            }
            return tempWord.ToString();
        }
    }
}
