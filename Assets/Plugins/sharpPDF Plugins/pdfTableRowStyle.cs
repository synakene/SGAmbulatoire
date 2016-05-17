using System;

using sharpPDF.Exceptions;
using sharpPDF.Enumerators;

namespace sharpPDF
{
	/// <summary>
	/// Class that represents the style of a table's row
	/// </summary>
	public class pdfTableRowStyle
	{
		
		private predefinedFont _fontType;
		private int _fontSize;
		private pdfColor _fontColor;
		private pdfColor _bgColor;
        private pdfColor _borderColor;

		/// <summary>
		/// Type of the Font
		/// </summary>
		public predefinedFont fontType
		{
			get
			{
				return _fontType;
			}			
		}

		/// <summary>
		/// Size of the Font
		/// </summary>
		public int fontSize
		{
			get
			{
				return _fontSize;
			}
		}

		/// <summary>
		/// Color of the Font
		/// </summary>
		public pdfColor fontColor
		{
			get
			{
				return _fontColor;
			}
		}

		/// <summary>
		/// Color of the BackGround
		/// </summary>
		public pdfColor bgColor
		{
			get
			{
				return _bgColor;
			}
		}

        /// <summary>
        /// Color of the BackGround
        /// </summary>
        public pdfColor borderColor
        {
            get
            {
                return _borderColor;
            }
        }

        /// <summary>
        /// Class's constructor
        /// </summary>
        public pdfTableRowStyle(predefinedFont fontType, int fontSize, pdfColor fontColor, pdfColor bgColor, pdfColor borderColor)
		{		
			_fontType = fontType;
			_fontSize = fontSize;
			_fontColor = fontColor;
			_bgColor = bgColor;
            _borderColor = borderColor;
        }
	}
}
