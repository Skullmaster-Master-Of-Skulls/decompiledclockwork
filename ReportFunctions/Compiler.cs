using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoComboBox;
using DynamicScreens;
using EncryptionClassLibrary;
using Microsoft.CSharp;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using UnivOleDb;

namespace ReportFunctions
{
	// Token: 0x02000053 RID: 83
	public class Compiler
	{
		// Token: 0x060004A7 RID: 1191 RVA: 0x0004FA3C File Offset: 0x0004EA3C
		public bool NeedsRecompile(string code_formLoaded, string code_preSave, string code_misc)
		{
			return !this.code_formLoaded.Equals(code_formLoaded) || !this.code_preSave.Equals(code_preSave) || !this.code_misc.Equals(code_misc);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0004FA7C File Offset: 0x0004EA7C
		public Compiler(string code_formLoaded, string code_preSave, string code_misc)
		{
			this.assembly = Compiler.CompileCode(code_formLoaded, code_preSave, code_misc);
			this._Compiled = this.assembly.CreateInstance("ClockWorkDynamicForms.ClockWorkDynamicFormsClass");
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0004FAD7 File Offset: 0x0004EAD7
		public void Init(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, PersonBaseDTO student, MyPanel p_data)
		{
			this.Init(da, tripleDES, student, p_data, new Dictionary<string, object>());
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0004FAEC File Offset: 0x0004EAEC
		public void Init(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, PersonBaseDTO student, MyPanel p_data, Dictionary<string, object> args)
		{
			MethodInfo method = this._Compiled.GetType().GetMethod("Init");
			method.Invoke(this._Compiled, new object[]
			{
				p_data,
				student,
				da,
				tripleDES,
				args
			});
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0004FB3C File Offset: 0x0004EB3C
		public void FormLoaded()
		{
			MethodInfo method = this._Compiled.GetType().GetMethod("FormLoaded");
			method.Invoke(this._Compiled, new object[0]);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0004FB74 File Offset: 0x0004EB74
		public bool PreSave()
		{
			MethodInfo method = this._Compiled.GetType().GetMethod("PreSave");
			object obj = method.Invoke(this._Compiled, new object[0]);
			return obj != null && obj is bool && (bool)obj;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0004FBD0 File Offset: 0x0004EBD0
		public static Assembly CompileCode(string code_formLoaded, string code_preSave, string code_misc)
		{
			Dictionary<string, string> providerOptions = new Dictionary<string, string>
			{
				{
					"CompilerVersion",
					"v3.5"
				}
			};
			ICodeCompiler codeCompiler = new CSharpCodeProvider(providerOptions).CreateCompiler();
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.ReferencedAssemblies.Add("system.dll");
			compilerParameters.ReferencedAssemblies.Add("system.data.dll");
			compilerParameters.ReferencedAssemblies.Add("system.xml.dll");
			compilerParameters.ReferencedAssemblies.Add("ClockWorkAPI.dll");
			compilerParameters.ReferencedAssemblies.Add("AutoComboBox.dll");
			compilerParameters.ReferencedAssemblies.Add("DynamicScreens.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
			compilerParameters.ReferencedAssemblies.Add("UnivOleDb.dll");
			compilerParameters.ReferencedAssemblies.Add("EncryptionClassLibrary.dll");
			compilerParameters.ReferencedAssemblies.Add("Common.Core.dll");
			compilerParameters.ReferencedAssemblies.Add("Common.ICore.dll");
			compilerParameters.ReferencedAssemblies.Add("Common.Public.dll");
			compilerParameters.ReferencedAssemblies.Add("ClockWorkServer.Contracts.dll");
			compilerParameters.GenerateExecutable = false;
			compilerParameters.GenerateInMemory = true;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("using System; \n");
			stringBuilder.Append("using System.Data; \n");
			stringBuilder.Append("using System.Data.SqlClient; \n");
			stringBuilder.Append("using System.Data.OleDb; \n");
			stringBuilder.Append("using System.Xml; \n");
			stringBuilder.Append("using System.Windows.Forms; \n");
			stringBuilder.Append("using UnivOleDb; \n");
			stringBuilder.Append("using EncryptionClassLibrary; \n");
			stringBuilder.Append("using TechnoPro.ClockWorkServer.Contracts.DTO.People; \n");
			stringBuilder.Append("namespace ClockWorkDynamicForms { \n");
			stringBuilder.Append("  public class ClockWorkDynamicFormsClass { \n");
			stringBuilder.Append("    private AutoComboBox.MyPanel _panel;\r\n    public AutoComboBox.MyPanel panel\r\n    {\r\n        get \r\n        {\r\n            if ( _panel == null )\r\n            {\r\n                var frm = Form.ActiveForm;\r\n                if ( frm != null )\r\n                {\r\n                    var x = frm.Controls.Find( \"p_data\", true );\r\n                    if ( x != null && x.Length > 0 && x[0] is AutoComboBox.MyPanel ) \r\n                        _panel = (AutoComboBox.MyPanel) x[0];\r\n                }\r\n            }\r\n            return _panel;\r\n        }\r\n        set { _panel = value; }\r\n    }\r\n");
			stringBuilder.Append("    public PersonBaseDTO student; \n");
			stringBuilder.Append("    public UnivDataAdapter da; \n");
			stringBuilder.Append("    public TripleDESEncryptionClass tripleDES; \n");
			stringBuilder.Append("    public System.Collections.Generic.Dictionary<string,object> args; \n");
			stringBuilder.Append("  public ClockWorkDynamicFormsClass( ) { } \n");
			stringBuilder.Append("  public void Init( AutoComboBox.MyPanel panel, PersonBaseDTO student, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, System.Collections.Generic.Dictionary<string,object> args ) { \n");
			stringBuilder.Append("    this.args = args;");
			stringBuilder.Append("    this.panel = panel; this.student = student; \n");
			stringBuilder.Append("    this.da = da; this.tripleDES = tripleDES; \n");
			stringBuilder.Append("  } \n");
			stringBuilder.Append("public int LuCourseId { get { return args == null || ! args.ContainsKey( \"lucid\" ) ? 0 : (int) args[ \"lucid\" ]; } } \n");
			stringBuilder.Append("  public void FormLoaded() {\n");
			stringBuilder.Append(code_formLoaded);
			stringBuilder.Append("  } \n");
			stringBuilder.Append("  public bool PreSave() {\n");
			stringBuilder.Append(code_preSave);
			stringBuilder.Append("  return true; } \n");
			stringBuilder.Append("  public Control FindControl( int controlId ) { return FindControl( panel, controlId ); } \n");
			stringBuilder.Append("  public Control FindControl( Control parent, int controlId ) { \n");
			stringBuilder.Append("    if ( parent.Tag is DataRow ) {\n");
			stringBuilder.Append("       DataRow dr = (DataRow) parent.Tag; \n");
			stringBuilder.Append("       if ( dr.Table.Columns.Contains( \"controlid\" ) ) { \n");
			stringBuilder.Append("           int cid = dr[ \"controlid\" ] == DBNull.Value ? 0 : (int) dr[ \"controlid\" ]; \n");
			stringBuilder.Append("           if ( cid == controlId ) return parent; \n");
			stringBuilder.Append("       } } \n");
			stringBuilder.Append("   foreach ( Control c in parent.Controls ) { Control found = FindControl( c, controlId ); if ( found != null ) return found; } \n");
			stringBuilder.Append("   return null; \n");
			stringBuilder.Append("  } \n");
			stringBuilder.Append("  public void SetControlValue( int controlId, bool boolVal ) { \n");
			stringBuilder.Append("    Control c = FindControl( controlId ); \n");
			stringBuilder.Append("    if ( c != null ) { \n");
			stringBuilder.Append("      if ( c is CheckBox ) ((CheckBox) c).Checked = boolVal; \n");
			stringBuilder.Append("    } } \n");
			stringBuilder.Append("  public void SetControlValue( int controlId, string stringVal ) { \n");
			stringBuilder.Append("    Control c = FindControl( controlId ); \n");
			stringBuilder.Append("    if ( c != null ) { \n");
			stringBuilder.Append("      if ( c is TextBox ) ((TextBox) c).Text = stringVal; \n");
			stringBuilder.Append("      else if ( c is ComboBox ) ((ComboBox) c).Text = stringVal; \n");
			stringBuilder.Append("      else if ( c is RichTextBox ) ((RichTextBox) c).Text = stringVal; \n");
			stringBuilder.Append("    } } \n");
			stringBuilder.Append("  public CheckBox FindCheckBox( int controlId ) { Control c = FindControl( controlId ); if ( c != null && c is CheckBox ) return (CheckBox) c; else if ( c != null && c is AutoComboBox.MyControls.AccommodationControl2 ) { AutoComboBox.MyControls.AccommodationControl2 ccc = (AutoComboBox.MyControls.AccommodationControl2) c; return ccc.GetCheckBox() ; } return null; } \n");
			stringBuilder.Append("  public TextBox FindTextBox( int controlId ) { Control c = FindControl( controlId ); if ( c != null && c is TextBox ) return (TextBox) c; else if ( c != null && c is AutoComboBox.MyControls.AccommodationControl2 ) { AutoComboBox.MyControls.AccommodationControl2 ccc = (AutoComboBox.MyControls.AccommodationControl2) c; return ccc.GetTextBox() ; } return null; } \n");
			stringBuilder.Append("  public AutoComboBox.AutoComboBox FindDropList( int controlId ) { Control c = FindControl( controlId ); if ( c != null && c is AutoComboBox.AutoComboBox ) return (AutoComboBox.AutoComboBox) c; else if ( c != null && c is AutoComboBox.MyControls.AccommodationControl2) { AutoComboBox.MyControls.AccommodationControl2 ccc = (AutoComboBox.MyControls.AccommodationControl2) c; return ccc.GetDropList(); } else return null; } \n");
			stringBuilder.Append("  public AutoComboBox.MyDateTimePicker FindDateTimePicker( int controlId ) { Control c = FindControl( controlId ); if ( c != null && c is AutoComboBox.MyDateTimePicker ) return (AutoComboBox.MyDateTimePicker) c; else if ( c != null && c is AutoComboBox.MyControls.AccommodationControl2) { AutoComboBox.MyControls.AccommodationControl2 ccc = (AutoComboBox.MyControls.AccommodationControl2) c; return ccc.GetDateTimePicker(); } else return null; } \n");
			stringBuilder.Append("  public Panel FindPanel( int controlId ) { Control c = FindControl( controlId ); if ( c != null && c is Panel ) return (Panel) c; else return null; } \n");
			stringBuilder.Append("  public RadioButton FindRadioButton( int controlId ) { Control c = FindControl( controlId ); if ( c != null && c is RadioButton ) return (RadioButton) c; else return null; } \n");
			stringBuilder.Append(code_misc);
			stringBuilder.Append(" } }");
			CompilerResults compilerResults = codeCompiler.CompileAssemblyFromSource(compilerParameters, stringBuilder.ToString());
			if (compilerResults.Errors.HasErrors)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("Error Compiling Expression: ");
				foreach (object obj in compilerResults.Errors)
				{
					CompilerError compilerError = (CompilerError)obj;
					stringBuilder2.AppendFormat("{0}\n", compilerError.ErrorText);
				}
				throw new Exception("Error Compiling Expression: " + stringBuilder2.ToString());
			}
			return compilerResults.CompiledAssembly;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0005009C File Offset: 0x0004F09C
		public static Compiler SetupNewCompiler(ref Dictionary<int, Compiler> compilersArchive, ScreenInfo screen, MyPanel p_data, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, PersonBaseDTO student)
		{
			return Compiler.SetupNewCompiler(ref compilersArchive, screen, p_data, da, tripleDES, student, new Dictionary<string, object>());
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000500C0 File Offset: 0x0004F0C0
		public static Compiler SetupNewCompiler(ref Dictionary<int, Compiler> compilersArchive, ScreenInfo screen, MyPanel p_data, UnivDataAdapter da, TripleDESEncryptionClass tripleDES, PersonBaseDTO student, Dictionary<string, object> args)
		{
			string value = screen.Args["code_formLoaded"];
			string value2 = screen.Args["code_preSave"];
			string value3 = screen.Args["code_misc"];
			Compiler compiler = null;
			if (!string.IsNullOrEmpty(value) || !string.IsNullOrEmpty(value2) || !string.IsNullOrEmpty(value3))
			{
				if (compilersArchive.ContainsKey(screen.screenNum))
				{
					compiler = compilersArchive[screen.screenNum];
				}
				if (compiler == null)
				{
					compiler = new Compiler(value, value2, value3);
					compilersArchive.Add(screen.screenNum, compiler);
				}
				else if (compiler.NeedsRecompile(value, value2, value3))
				{
					compilersArchive.Remove(screen.screenNum);
					compiler = new Compiler(value, value2, value3);
					compilersArchive.Add(screen.screenNum, compiler);
				}
				compiler.Init(da, tripleDES, student, p_data, args);
				compiler.FormLoaded();
			}
			return compiler;
		}

		// Token: 0x04000288 RID: 648
		private Assembly assembly;

		// Token: 0x04000289 RID: 649
		private object _Compiled;

		// Token: 0x0400028A RID: 650
		private string code_formLoaded = "";

		// Token: 0x0400028B RID: 651
		private string code_preSave = "";

		// Token: 0x0400028C RID: 652
		private string code_misc = "";
	}
}
