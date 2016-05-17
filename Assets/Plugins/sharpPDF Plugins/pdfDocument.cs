using System;
using System.IO;
using System.Text;
using System.Collections;

using sharpPDF.Exceptions;
using sharpPDF.Enumerators;
using UnityEngine;
using System.Collections.Generic;

namespace sharpPDF
{
	/// <summary>
	/// A Class that implements a PDF document.
	/// </summary>
	public class pdfDocument
	{
		private string _title;
		private string _author;
		private bool _openBookmark;
		private pdfHeader _header;
		private pdfInfo _info;
		private pdfOutlines _outlines = new pdfOutlines();
		private pdfPageTree _pageTree;
		private pdfTrailer _trailer;
		private ArrayList _fonts = new ArrayList();
		private ArrayList _pages = new ArrayList();
		private pdfPageMarker _pageMarker = null;
		private pdfPersistentPage _persistentPage = null;

        private byte[] pdfdata;
        public static char[] pdfDataChar;

		/// <summary>
		/// Document's page marker
		/// </summary>
		public pdfPageMarker pageMarker
		{
			set
			{
				_pageMarker = value;
			}
		}

		/// <summary>
		/// Document's persistent page
		/// </summary>
		public pdfPersistentPage persistentPage
		{
			set
			{
				_persistentPage = value;
			}
		}

		/// <summary>
		/// Collection of pdf's page
		/// </summary>
		public pdfPage this[int index]
		{
			get
			{
				return (pdfPage)_pages[index];
			}
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="author">Author of the document</param>
		/// <param name="title">Title of the document</param>
		public pdfDocument(string title, string author)
		{			
			_title = title;
			_author = author;
			_openBookmark = false;
		}

		/// <summary>
		/// Class's constructor
		/// </summary>
		/// <param name="author">Author of the document</param>
		/// <param name="title">Title of the document</param>
		/// <param name="openBookmark">Allow to show directly bookmarks near the document</param>
		public pdfDocument(string title, string author, bool openBookmark)
		{			
			_title = title;
			_author = author;
			_openBookmark = openBookmark;
		}

		/// <summary>
		/// Class's destructor
		/// </summary>
		~pdfDocument()
		{
			_header = null;
			_info = null;
			_outlines = null;
            _fonts = null;
            _pages = null;
            _pageTree = null;
            _trailer = null;
			_title = null;
			_author = null;
			_pageMarker = null;
			_persistentPage = null;

		}

        private byte[] ReadToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        private long getNbBytes(string data)
        {
            long nbBytes = 0;
            nbBytes += ASCIIEncoding.ASCII.GetByteCount(data);
            return nbBytes;
        }

        /// <summary>
        /// Method that writes the PDF document on the stream
        /// </summary>
        /// <param name="outStream">Output stream</param>
        public List<string> CustumCreatePDF()
        {
            //BufferedStream _myBuffer = null;
            long _bufferLength = 0;
            List<string> dataChar = new List<string>();

            initializeObjects();
            try
            {
                //Bufferedstream's initialization 
                //_myBuffer = new BufferedStream(outStream);


                //PDF's definition
                //_bufferLength += writeToBuffer(_myBuffer, @"%PDF-1.4" + Convert.ToChar(13) + Convert.ToChar(10));
                dataChar.Add(@"%PDF-1.4" + Convert.ToChar(13) + Convert.ToChar(10));
                string temp = @"%PDF-1.4" + Convert.ToChar(13) + Convert.ToChar(10);
                _bufferLength += getNbBytes(temp);

                //PDF's header object
                //_trailer.addObject(_bufferLength.ToString());
                //_bufferLength += writeToBuffer(_myBuffer, _header.getText());
                _trailer.addObject(_bufferLength.ToString());
                dataChar.Add(_header.getText());
                _bufferLength += getNbBytes(_header.getText());

                //PDF's info object
                //_trailer.addObject(_bufferLength.ToString());
                //_bufferLength += writeToBuffer(_myBuffer, _info.getText());
                _trailer.addObject(_bufferLength.ToString());
                dataChar.Add(_info.getText());
                _bufferLength += getNbBytes(_info.getText());


                //PDF's outlines object
                //_trailer.addObject(_bufferLength.ToString());
                //_bufferLength += writeToBuffer(_myBuffer, _outlines.getText());
                _trailer.addObject(_bufferLength.ToString());
                dataChar.Add(_outlines.getText());
                _bufferLength += getNbBytes(_outlines.getText());

                //PDF's bookmarks
                foreach (pdfBookmarkNode Node in _outlines.getBookmarks())
                {
                    //_trailer.addObject(_bufferLength.ToString());
                    //_bufferLength += writeToBuffer(_myBuffer, Node.getText());
                    _trailer.addObject(_bufferLength.ToString());
                    dataChar.Add(Node.getText());
                    _bufferLength += getNbBytes(Node.getText());
                }

                //Fonts's initialization
                foreach (pdfFont font in _fonts)
                {
                    //_trailer.addObject(_bufferLength.ToString());
                    //_bufferLength += writeToBuffer(_myBuffer, font.getText());
                    _trailer.addObject(_bufferLength.ToString());
                    dataChar.Add(font.getText());
                    _bufferLength += getNbBytes(font.getText());
                }
                //PDF's pagetree object
                //_trailer.addObject(_bufferLength.ToString());
                //_bufferLength += writeToBuffer(_myBuffer, _pageTree.getText());
                _trailer.addObject(_bufferLength.ToString());
                dataChar.Add(_pageTree.getText());
                _bufferLength += getNbBytes(_pageTree.getText());

                //Generation of PDF's pages
                foreach (pdfPage page in _pages)
                {
                    //_trailer.addObject(_bufferLength.ToString());
                    //_bufferLength += writeToBuffer(_myBuffer, page.getText());
                    _trailer.addObject(_bufferLength.ToString());
                    dataChar.Add(page.getText());
                    _bufferLength += getNbBytes(page.getText());

                    foreach (pdfElement element in page.elements)
                    {
                        //if (element.GetType().Name == "imageElement") {
                        if (element is imageElement)
                        {
                            //_trailer.addObject(_bufferLength.ToString()); //ok
                            //_bufferLength += writeToBuffer(_myBuffer, element.getText()); //ok
                            //_trailer.addObject(_bufferLength.ToString()); //ok
                            //_bufferLength += writeToBuffer(_myBuffer, ((imageElement)element).getXObjectText()); //ok
                            //_bufferLength += writeToBuffer(_myBuffer, "stream" + Convert.ToChar(13) + Convert.ToChar(10)); //ok
                            //_bufferLength += writeToBuffer(_myBuffer, ((imageElement)element).content); //fail
                            //_bufferLength += writeToBuffer(_myBuffer, Convert.ToChar(13).ToString()); //ok
                            //_bufferLength += writeToBuffer(_myBuffer, Convert.ToChar(10).ToString()); //ok
                            //_bufferLength += writeToBuffer(_myBuffer, "endstream" + Convert.ToChar(13) + Convert.ToChar(10)); //ok
                            //_bufferLength += writeToBuffer(_myBuffer, "endobj" + Convert.ToChar(13) + Convert.ToChar(10));
                            _trailer.addObject(_bufferLength.ToString());
                            dataChar.Add(element.getText());
                            _bufferLength += getNbBytes(element.getText());
                            _trailer.addObject(_bufferLength.ToString());
                            dataChar.Add(((imageElement)element).getXObjectText());
                            _bufferLength += getNbBytes(((imageElement)element).getXObjectText());
                            dataChar.Add("stream" + Convert.ToChar(13) + Convert.ToChar(10));
                            _bufferLength += getNbBytes("stream" + Convert.ToChar(13) + Convert.ToChar(10));
                            dataChar.Add(Convert.ToChar(13).ToString());
                            _bufferLength += getNbBytes(Convert.ToChar(13).ToString());
                            dataChar.Add(Convert.ToChar(10).ToString());
                            _bufferLength += getNbBytes(Convert.ToChar(10).ToString());
                            dataChar.Add("endstream" + Convert.ToChar(13) + Convert.ToChar(10));
                            _bufferLength += getNbBytes("endstream" + Convert.ToChar(13) + Convert.ToChar(10));
                            dataChar.Add("endobj" + Convert.ToChar(13) + Convert.ToChar(10));
                            _bufferLength += getNbBytes("endobj" + Convert.ToChar(13) + Convert.ToChar(10));
                        }
                        else
                        {
                            //_trailer.addObject(_bufferLength.ToString());
                            //_bufferLength += writeToBuffer(_myBuffer, element.getText());
                            _trailer.addObject(_bufferLength.ToString());
                            dataChar.Add(element.getText());
                            _bufferLength += getNbBytes(element.getText());
                        }
                    }

                }
                //PDF's trailer object
                _trailer.xrefOffset = _bufferLength;
                //_bufferLength += writeToBuffer(_myBuffer, _trailer.getText());
                dataChar.Add(_trailer.getText());
                _bufferLength += getNbBytes(_trailer.getText());

                //Buffer's flush
                //_myBuffer.Flush();

                //Debug.Log("pdfdata.Length : " + pdfdata.Length);

                return dataChar;

            }
            catch (IOException ex)
            {
                throw new pdfWritingErrorException("Errore nella scrittura del PDF", ex);
            }
            finally
            {
                //if (_myBuffer != null)
                //{
                //    _myBuffer.Close();
                //    _myBuffer = null;
                //}
            }
        }


        /// <summary>
        /// Method that writes the PDF document on the stream
        /// </summary>
        /// <param name="outStream">Output stream</param>
        public void createPDF(Stream outStream)
        {
            BufferedStream _myBuffer = null;
            long _bufferLength = 0;

            initializeObjects();
            try
            {
                //Bufferedstream's initialization 
                _myBuffer = new BufferedStream(outStream);

                //PDF's definition
                _bufferLength += writeToBuffer(_myBuffer, @"%PDF-1.4" + Convert.ToChar(13) + Convert.ToChar(10));

                //PDF's header object
                _trailer.addObject(_bufferLength.ToString());
                _bufferLength += writeToBuffer(_myBuffer, _header.getText());

                //PDF's info object
                _trailer.addObject(_bufferLength.ToString());
                _bufferLength += writeToBuffer(_myBuffer, _info.getText());

                //PDF's outlines object
                _trailer.addObject(_bufferLength.ToString());
                _bufferLength += writeToBuffer(_myBuffer, _outlines.getText());

                //PDF's bookmarks
                foreach (pdfBookmarkNode Node in _outlines.getBookmarks())
                {
                    _trailer.addObject(_bufferLength.ToString());
                    _bufferLength += writeToBuffer(_myBuffer, Node.getText());
                }

                //Fonts's initialization
                foreach (pdfFont font in _fonts)
                {
                    _trailer.addObject(_bufferLength.ToString());
                    _bufferLength += writeToBuffer(_myBuffer, font.getText());
                }
                //PDF's pagetree object
                _trailer.addObject(_bufferLength.ToString());
                _bufferLength += writeToBuffer(_myBuffer, _pageTree.getText());

                //Generation of PDF's pages
                foreach (pdfPage page in _pages)
                {
                    _trailer.addObject(_bufferLength.ToString());
                    _bufferLength += writeToBuffer(_myBuffer, page.getText());

                    foreach (pdfElement element in page.elements)
                    {
                        //if (element.GetType().Name == "imageElement") {
                        if (element is imageElement)
                        {
                            _trailer.addObject(_bufferLength.ToString());
                            _bufferLength += writeToBuffer(_myBuffer, element.getText());
                            _trailer.addObject(_bufferLength.ToString());
                            _bufferLength += writeToBuffer(_myBuffer, ((imageElement)element).getXObjectText());
                            _bufferLength += writeToBuffer(_myBuffer, "stream" + Convert.ToChar(13) + Convert.ToChar(10));
                            _bufferLength += writeToBuffer(_myBuffer, ((imageElement)element).content);
                            _bufferLength += writeToBuffer(_myBuffer, Convert.ToChar(13).ToString());
                            _bufferLength += writeToBuffer(_myBuffer, Convert.ToChar(10).ToString());
                            _bufferLength += writeToBuffer(_myBuffer, "endstream" + Convert.ToChar(13) + Convert.ToChar(10));
                            _bufferLength += writeToBuffer(_myBuffer, "endobj" + Convert.ToChar(13) + Convert.ToChar(10));
                        }
                        else
                        {
                            _trailer.addObject(_bufferLength.ToString());
                            _bufferLength += writeToBuffer(_myBuffer, element.getText());
                        }
                    }

                }
                //PDF's trailer object
                _trailer.xrefOffset = _bufferLength;
                _bufferLength += writeToBuffer(_myBuffer, _trailer.getText());
                //Buffer's flush
                _myBuffer.Flush();
            }
            catch (IOException ex)
            {
                throw new pdfWritingErrorException("Errore nella scrittura del PDF", ex);
            }
            finally
            {
                if (_myBuffer != null)
                {
                    _myBuffer.Close();
                    _myBuffer = null;
                }
            }
        }

        /// <summary>
        /// Method that writes the PDF document on a file
        /// </summary>
        /// <param name="outputFile">String that represents the name of the output file</param>
        public void createPDF(string outputFile)
        {
            FileStream _myFileOut = null; ;
            try
            {
                _myFileOut = new FileStream(outputFile, FileMode.Create);
                createPDF(_myFileOut);
            }
            catch (IOException exIO)
            {
                throw new pdfWritingErrorException("Errore nella scrittura del file", exIO);
            }
            catch (pdfWritingErrorException exPDF)
            {
                throw new pdfWritingErrorException("Errore nella scrittura del PDF", exPDF);
            }
            finally
            {
                if (_myFileOut != null)
                {
                    _myFileOut.Close();
                    _myFileOut = null;
                }
            }
        }

        /// <summary>
        /// Private method for the initialization of all PDF objects
        /// </summary>
        private void initializeObjects()
		{

			//Page's counters
			int	pageIndex = 1;
			int	pageNum = _pages.Count;

			int counterID = 0;
			//header			
			_header = new pdfHeader(_openBookmark);
            _header.objectIDHeader = 1;						
			_header.objectIDInfo = 2;
            _header.objectIDOutlines = 3;
			//Info
			_info = new pdfInfo(_title, _author);
			_info.objectIDInfo = 2;	
			//Outlines			
			_outlines.objectIDOutlines = 3;
			counterID = 4;
			//Bookmarks	
			counterID = _outlines.initializeOutlines(counterID);			
			//fonts
            for (int i= 0; i < 12; i++)
			{
                _fonts.Add(new pdfFont((predefinedFont)(i + 1), i + 1));
				((pdfFont)_fonts[i]).objectID = counterID;
				counterID++;
			}
			//pagetree
            _pageTree = new pdfPageTree();
			_pageTree.objectID = counterID;
            _header.pageTreeID = counterID;
            counterID++;
			//pages
            foreach(pdfPage page in _pages)
			{
                page.objectID = counterID;
				page.pageTreeID = _pageTree.objectID;
				page.addFonts(_fonts);
                _pageTree.addPage(counterID);
                counterID++;
				
				//Add page's Marker
				if (_pageMarker != null) {
					page.addText(_pageMarker.getMarker(pageIndex, pageNum),_pageMarker.coordX, _pageMarker.coordY,_pageMarker.fontType, _pageMarker.fontSize, _pageMarker.fontColor);
				}

				//Add persistent elements
				if (_persistentPage != null) {
					page.elements.AddRange(_persistentPage.persistentElements);
				}

				//page's elements
                foreach (pdfElement element in page.elements)
				{
                    element.objectID = counterID;
                    counterID++;
					//Imageobject
                    if (element.GetType().Name == "imageElement")
					{
                        ((imageElement)element).xObjectID = counterID;
                        counterID++;					
					}
				}

				//Update page's index counter
				pageIndex += 1;
			}
			//trailer
			_trailer = new pdfTrailer(counterID - 1);			
		}

		/// <summary>
		/// Method that creates a new page
		/// </summary>
		/// <returns>New PDF's page</returns>
		public pdfPage addPage()
		{
			_pages.Add(new pdfPage());
			return (pdfPage)_pages[_pages.Count - 1];
		}

		/// <summary>
		/// Method that creates a new page
		/// </summary>
		/// <returns>New PDF's page</returns>
		/// <param name="height">Height of the new page</param>
		/// <param name="width">Width of the new page</param>
		public pdfPage addPage(int height, int width)
		{
			_pages.Add(new pdfPage(height, width));
			return (pdfPage)_pages[_pages.Count - 1];
		}

		public void addBookmark(pdfBookmarkNode Bookmark)
		{
			_outlines.addBookmark(Bookmark);
		}

		/// <summary>
		/// Method that writes into the buffer a string
		/// </summary>
		/// <param name="myBuffer">Output Buffer</param>
		/// <param name="stringContent">String that contains the informations</param>
		/// <returns>The number of the bytes written in the Buffer</returns>
		private long writeToBuffer(BufferedStream myBuffer, string stringContent)
		{
			ASCIIEncoding myEncoder = new ASCIIEncoding();
			byte[] arrTemp;
			try	{
				arrTemp = myEncoder.GetBytes(stringContent);					
				myBuffer.Write(arrTemp, 0, arrTemp.Length);
				return arrTemp.Length;
			} catch (IOException ex) {
				throw new pdfBufferErrorException("Errore nella scrittura del Buffer", ex);
			}			
		}

		/// <summary>
		/// Method that writes into the buffer a string
		/// </summary>
		/// <param name="myBuffer">Output Buffer</param>
		/// <param name="byteContent">A Byte array that contains the informations</param>
		/// <returns>The number of the bytes written in the Buffer</returns>
		private long writeToBuffer(BufferedStream myBuffer, byte[] byteContent)
		{
			try	{
				myBuffer.Write(byteContent, 0, byteContent.Length);
				return byteContent.Length;
			} catch (IOException ex) {
				throw new pdfBufferErrorException("Errore nella scrittura del Buffer", ex);
			}					
		}

	}
}
