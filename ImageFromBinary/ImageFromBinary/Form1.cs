using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageFromBinary
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				if (textBox1.Text.Trim().Length > 0)
				{
					//var textFromBinary = GetStringToByteArray(textBox1.Text.Trim());

					//Image image = null;
					//using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
					//{
					//	ms.Write(textFromBinary, 0, textFromBinary.Length);
					//	ms.Position = 0L;

					//	image = new Bitmap(ms);
					//}

					//pictureBox1.Image = image;

					Image image = CreateImage(StrToByteArray(textBox1.Text.Trim()));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public static byte[] StrToByteArray(string str)
		{
			System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
			return encoding.GetBytes(str);
		}

		public static Image CreateImage(byte[] imageData)
		{
			Image image;
			using (MemoryStream inStream = new MemoryStream())
			{
				inStream.Write(imageData, 0, imageData.Length);

				image = Bitmap.FromStream(inStream);
			}

			return image;
		}


		//public static String GetByteString(byte[] bytes)
		//{
		//	if (bytes == null || bytes.Length == 0)
		//		return String.Empty;

		//	System.Text.StringBuilder sb = new StringBuilder(bytes.Length * 2);
		//	sb
		//}

		public static byte[] GetStringToByteArray(String str)
		{
			if (String.IsNullOrEmpty(str))
				return null;

			str = str.Substring("0x".Length);

			byte[] byteArray = new byte[str.Length / 2];

			for (int i = 0; i < byteArray.Length; i++)
			{
				try
				{
					byteArray[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
				}
				catch
				{
					throw new ArgumentException("hex is not a valid hex number!", "hex");
				}
			}

			return byteArray;
		}
	}
}
