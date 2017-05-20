//
// Authors:	 
//	  Ivan N. Zlatev (contact i-nZ.net)
//
// (C) 2007 Ivan N. Zlatev

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;

namespace mwf_designer
{
	internal static class TemplateManager
	{
		public static void WriteCode(string templateName, string fileName, string codeBehindFileName,
			string namespaceName, string className)
		{
			string main = null;
			string codeBehind = null;
			switch (templateName)
			{
				case "Form C#":
					main = _formCSharpMain;
					codeBehind = _formCSharpCodeBehind;
					break;
				default:
					return;
			}

			while (main.IndexOf("${namespace}") != -1)
				main = main.Replace("${namespace}", namespaceName);
			while (codeBehind.IndexOf("${namespace}") != -1)
				codeBehind = codeBehind.Replace("${namespace}", namespaceName);

			while (main.IndexOf("${class}") != -1)
				main = main.Replace("${class}", className);
			while (codeBehind.IndexOf("${class}") != -1)
				codeBehind = codeBehind.Replace("${class}", className);

			// write main file
			FileStream stream = File.OpenWrite(fileName);
			byte[] bytes = Encoding.Default.GetBytes(main);
			stream.Write(bytes, 0, bytes.Length);
			stream.Close();
			stream.Dispose();

			// write codebehind file
			stream = File.OpenWrite(codeBehindFileName);
			bytes = Encoding.Default.GetBytes(codeBehind);
			stream.Write(bytes, 0, bytes.Length);
			stream.Close();
			stream.Dispose();
		}

		public static string[] AvailableTemplates
		{
			get
			{
				return new string[]
				{
					"Form C#",
				};
			}
		}


		private static string _formCSharpMain = @"using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ${namespace}
{
	public partial class ${class} : Form
	{
		public ${class}()
		{
			InitializeComponent();
		}
	}
}
";


		private static string _formCSharpCodeBehind = @"namespace ${namespace}
{
	partial class ${class}
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Text = ""${class}"";
		}

		#endregion
	}
}";
	}
}
