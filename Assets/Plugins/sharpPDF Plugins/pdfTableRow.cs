using System;
using System.Collections;
using UnityEngine;
using sharpPDF.Exceptions;
using sharpPDF.Enumerators;

namespace sharpPDF
{
	/// <summary>
	/// A Class that implements an abstract table's row object
	/// </summary>
	public class pdfTableRow : IWritable, IEnumerable
	{

        private pdfTableRowStyle _pdfTableRowStyleProp;
        // Takes ForeColor and BackColor of row
        public pdfTableRowStyle RowStyleProp
        {
            get { return _pdfTableRowStyleProp; }
            set { _pdfTableRowStyleProp = value; }
        }

        /// <summary>
        /// ArrayList of Columns
        /// </summary>
        protected ArrayList _cols = new ArrayList();

		/// <summary>
		/// Class's constructor
		/// </summary>
		internal pdfTableRow()
		{
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="tableHeader">Row Template based on Table's Header</param>
		internal pdfTableRow(pdfTableHeader tableHeader)
		{
			foreach(pdfTableColumn myCol in tableHeader)
			{
				_cols.Add(new pdfTableColumn("", myCol.columnAlign, myCol.columnSize));
			}
		}

        /// <summary>
        /// Class's constructor
        /// </summary>
        /// <param name="tableHeader">Row Template based on Table's Header</param>
        /// <param name="number">Number of columns (size)</param>
        internal pdfTableRow(pdfTableHeader tableHeader, int number)
        {
            _cols.Add(new pdfTableColumn("", tableHeader[0].columnAlign, tableHeader[0].columnSize * number));
        }

        /// <summary>
        /// Indexer of the pdfTableRow that represents its columns
        /// </summary>
        public pdfTableColumn this[int index]
		{
			get
			{
				if (index < 0 || index >= _cols.Count) {
                    throw new pdfBadColumnIndexException();
				} else {
					return (pdfTableColumn)_cols[index];
				}
			}

			set
			{
				if (index < 0 || index >= _cols.Count) {
					throw new pdfBadColumnIndexException();
				} else {
					_cols[index] = value;
				}
			}
		}		

		/// <summary>
		/// The number of columns
		/// </summary>
		public int columnsCount
		{	
			get
			{
				return _cols.Count;
			}
		}

		/// <summary>
		/// Enumerator of the column's collection
		/// </summary>
		/// <returns>A IEnumerator of the indexed property</returns>
		public IEnumerator GetEnumerator()
		{
			return _cols.GetEnumerator();
		}

		/// <summary>
		/// Method that returns the PDF codes to write the tableRow in the document
		/// </summary>
		/// <returns>String that contains PDF codes</returns>
		public string getText() {
			return null;
		}
	}
}
