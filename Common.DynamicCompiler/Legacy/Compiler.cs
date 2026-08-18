using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using EncryptionClassLibrary;
using Microsoft.CSharp;

namespace TechnoPro.Common.DynamicCompiler.Legacy
{
	// Token: 0x0200000C RID: 12
	public class Compiler
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002F24 File Offset: 0x00001124
		public bool NeedsRecompile(string code_formLoaded, string code_preSave, string code_misc)
		{
			return (this._codeFormLoaded ?? "") != (code_formLoaded ?? "") || (this._codePreSave ?? "") != (code_preSave ?? "") || (this._codeMisc ?? "") != (code_misc ?? "");
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F98 File Offset: 0x00001198
		public Compiler(string code_formLoaded, string code_preSave, string code_misc, string BinPath)
		{
			this._codeFormLoaded = code_formLoaded;
			this._codePreSave = code_preSave;
			this._codeMisc = code_misc;
			Assembly assembly = Compiler.CompileCode(code_formLoaded, code_preSave, code_misc, BinPath);
			this._compiled = assembly.CreateInstance("ClockWorkDynamicForms.ClockWorkDynamicFormsClass");
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002FE0 File Offset: 0x000011E0
		public void Init(object da, IEncryption tripleDes, object student, object pData, Dictionary<string, object> args)
		{
			MethodInfo method = this._compiled.GetType().GetMethod("Init");
			method.Invoke(this._compiled, new object[]
			{
				pData,
				student,
				da,
				tripleDes,
				args
			});
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003030 File Offset: 0x00001230
		public void Init2(object da, object tripleDes, object student, object pData, Dictionary<string, object> args)
		{
			MethodInfo method = this._compiled.GetType().GetMethod("Init");
			method.Invoke(this._compiled, new object[]
			{
				pData,
				student,
				da,
				(IEncryption)tripleDes,
				args
			});
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003084 File Offset: 0x00001284
		public void FormLoaded()
		{
			MethodInfo method = this._compiled.GetType().GetMethod("FormLoaded");
			method.Invoke(this._compiled, new object[0]);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000030BC File Offset: 0x000012BC
		public bool PreSave()
		{
			MethodInfo method = this._compiled.GetType().GetMethod("PreSave");
			object obj = method.Invoke(this._compiled, new object[0]);
			bool flag = obj is bool;
			return flag && (bool)obj;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003110 File Offset: 0x00001310
		public static Assembly CompileCode(string code_formLoaded, string code_preSave, string code_misc, string BinPath)
		{
			return Compiler.CompileCodeLegacy(code_formLoaded, code_preSave, code_misc, BinPath);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000312C File Offset: 0x0000132C
		private static Assembly CompileCodeLegacy(string code_formLoaded, string code_preSave, string code_misc, string BinPath)
		{
			Dictionary<string, string> providerOptions = new Dictionary<string, string>
			{
				{
					"CompilerVersion",
					"v4.0"
				}
			};
			ICodeCompiler codeCompiler = new CSharpCodeProvider(providerOptions).CreateCompiler();
			CompilerParameters compilerParameters = new CompilerParameters
			{
				WarningLevel = 3,
				CompilerOptions = (string.IsNullOrEmpty(BinPath) ? "/define:NET45" : ("/lib:\"" + BinPath + "\" /define:NET45"))
			};
			compilerParameters.ReferencedAssemblies.Add("system.dll");
			compilerParameters.ReferencedAssemblies.Add("system.data.dll");
			compilerParameters.ReferencedAssemblies.Add("system.xml.dll");
			compilerParameters.ReferencedAssemblies.Add("ClockWorkAPI.dll");
			compilerParameters.ReferencedAssemblies.Add("AutoComboBox.dll");
			compilerParameters.ReferencedAssemblies.Add("Common.UI.WinForms.Entity.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Drawing.dll");
			compilerParameters.ReferencedAssemblies.Add("DynamicScreens.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
			compilerParameters.ReferencedAssemblies.Add("UnivOleDb.dll");
			compilerParameters.ReferencedAssemblies.Add("EncryptionClassLibrary.dll");
			compilerParameters.ReferencedAssemblies.Add("Common.Core.dll");
			compilerParameters.ReferencedAssemblies.Add("Common.ICore.dll");
			compilerParameters.ReferencedAssemblies.Add("Common.Public.dll");
			compilerParameters.ReferencedAssemblies.Add("Common.UI.WinForms.CoreComponents.dll");
			compilerParameters.ReferencedAssemblies.Add("ClockWorkServer.Contracts.dll");
			compilerParameters.ReferencedAssemblies.Add("Common.UI.WinForms.dll");
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
			stringBuilder.Append("using TechnoPro.Common.UI.WinForms.DynamicForms.Controls.Legacy; \n");
			stringBuilder.Append("namespace ClockWorkDynamicForms { \n");
			stringBuilder.Append("  public class ClockWorkDynamicFormsClass { \n");
			stringBuilder.Append("    private MyPanel _panel;\r\n    public MyPanel panel\r\n    {\r\n        get \r\n        {\r\n            if ( _panel == null )\r\n            {\r\n                var frm = Form.ActiveForm;\r\n                if ( frm != null )\r\n                {\r\n                    var x = frm.Controls.Find( \"pData\", true );\r\n                    if ( x != null && x.Length > 0 && x[0] is MyPanel ) \r\n                        _panel = (MyPanel) x[0];\r\n                }\r\n            }\r\n            return _panel;\r\n        }\r\n        set { _panel = value; }\r\n    }\r\n");
			stringBuilder.Append("    public PersonBaseDTO student; \n");
			stringBuilder.Append("    public UnivDataAdapter da; \n");
			stringBuilder.Append("    public IEncryption tripleDes; public IEncryption tripleDES; \n");
			stringBuilder.Append("    public System.Collections.Generic.Dictionary<string,object> args; \n");
			stringBuilder.Append("  public ClockWorkDynamicFormsClass( ) { } \n");
			stringBuilder.Append("  public void Init( MyPanel panel, PersonBaseDTO student, UnivDataAdapter da, IEncryption tripleDes, System.Collections.Generic.Dictionary<string,object> args ) { \n");
			stringBuilder.Append("    var tripleDES = tripleDes;");
			stringBuilder.Append("    this.args = args;");
			stringBuilder.Append("    this.panel = panel; this.student = student; \n");
			stringBuilder.Append("    this.da = da; this.tripleDes = tripleDes; this.tripleDES = tripleDes; \n");
			stringBuilder.Append("  } \n");
			stringBuilder.Append("public int LuCourseId { get { return args == null || ! args.ContainsKey( \"lucid\" ) ? 0 : (int) args[ \"lucid\" ]; } } \n");
			stringBuilder.Append("  public void FormLoaded() {\n");
			stringBuilder.Append(code_formLoaded);
			stringBuilder.Append("  } \n");
			stringBuilder.Append("  public bool PreSave() {\n");
			stringBuilder.Append(code_preSave);
			stringBuilder.Append("  return true; } \n");
			stringBuilder.Append(Compiler.GetUserFunctions());
			stringBuilder.Append(code_misc);
			stringBuilder.Append(" } }");
			CompilerResults compilerResults = codeCompiler.CompileAssemblyFromSource(compilerParameters, stringBuilder.ToString());
			bool hasErrors = compilerResults.Errors.HasErrors;
			if (hasErrors)
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

		// Token: 0x0600005C RID: 92 RVA: 0x00003518 File Offset: 0x00001718
		public static string GetUserFunctions()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("  public Control FindControl( int controlId ) { return FindControl( panel, controlId ); } \n");
			stringBuilder.Append("  public Control FindControl( string controlCaption ) { return FindControl( panel, controlCaption ); } \n");
			stringBuilder.Append("  public Control FindControl( Control parent, string controlCaption ) { \n");
			stringBuilder.Append("    if ( parent.Tag is DataRow ) {\n");
			stringBuilder.Append("       DataRow dr = (DataRow) parent.Tag; \n");
			stringBuilder.Append("       if ( dr.Table.Columns.Contains( \"controlcaption\" ) ) { \n");
			stringBuilder.Append("           var cc = dr[ \"controlcaption\" ] is DBNull ? string.Empty : dr[ \"controlcaption\" ].ToString(); \n");
			stringBuilder.Append("           if ( cc.Equals( controlCaption, StringComparison.OrdinalIgnoreCase ) ) return parent; \n");
			stringBuilder.Append("       } } \n");
			stringBuilder.Append("   foreach ( Control c in parent.Controls ) { Control found = FindControl( c, controlCaption ); if ( found != null ) return found; } \n");
			stringBuilder.Append("   return null; \n");
			stringBuilder.Append("  } \n");
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
			stringBuilder.Append("  public CheckBox FindCheckBox( int controlId ) { Control c = FindControl( controlId ); if ( c != null && c is CheckBox ) return (CheckBox) c; else if ( c != null && c is TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse ) { TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse ccc = (TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse) c; return ccc.GetCheckBox() ; } return null; } \n");
			stringBuilder.Append("  public TextBox FindTextBox( int controlId ) { Control c = FindControl( controlId ); if ( c != null && c is TextBox ) return (TextBox) c; else if ( c != null && c is TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse ) { TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse ccc = (TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse) c; return ccc.GetTextBox() ; } return null; } \n");
			stringBuilder.Append("  public TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.CtrlAutoComboBox FindDropList( int controlId ) { Control c = FindControl( controlId ); if ( c != null && c is TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.CtrlAutoComboBox ) return (TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.CtrlAutoComboBox) c; else if ( c != null && c is TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse) { TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse ccc = (TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse) c; return ccc.GetDropList(); } else return null; } \n");
			stringBuilder.Append("  public TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.CtrlDateTimePickerLegacy FindDateTimePicker( int controlId ) { Control c = FindControl( controlId ); if ( c != null && c is TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.CtrlDateTimePickerLegacy ) return (TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.CtrlDateTimePickerLegacy) c; else if ( c != null && c is TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse) { TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse ccc = (TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse) c; return ccc.GetDateTimePicker(); } else return null; } \n");
			stringBuilder.Append("  public Panel FindPanel( int controlId ) { Control c = FindControl( controlId ); if ( c != null && c is Panel ) return (Panel) c; else return null; } \n");
			stringBuilder.Append("  public RadioButton FindRadioButton( int controlId ) { Control c = FindControl( controlId ); if ( c != null && c is RadioButton ) return (RadioButton) c; else return null; } \n");
			stringBuilder.Append("  public CheckBox FindCheckBox( string controlCaption ) { Control c = FindControl( controlCaption ); if ( c != null && c is CheckBox ) return (CheckBox) c; else if ( c != null && c is TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse ) { TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse ccc = (TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse) c; return ccc.GetCheckBox() ; } return null; } \n");
			stringBuilder.Append("  public TextBox FindTextBox( string controlCaption ) { Control c = FindControl( controlCaption ); if ( c != null && c is TextBox ) return (TextBox) c; else if ( c != null && c is TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse ) { TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse ccc = (TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse) c; return ccc.GetTextBox() ; } return null; } \n");
			stringBuilder.Append("  public TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.CtrlAutoComboBox FindDropList( string controlCaption ) { Control c = FindControl( controlCaption ); if ( c != null && c is TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.CtrlAutoComboBox ) return (TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.CtrlAutoComboBox) c; else if ( c != null && c is TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse) { TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse ccc = (TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse) c; return ccc.GetDropList(); } else return null; } \n");
			stringBuilder.Append("  public TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.CtrlDateTimePickerLegacy FindDateTimePicker( string controlCaption ) { Control c = FindControl( controlCaption ); if ( c != null && c is TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.CtrlDateTimePickerLegacy ) return (TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.CtrlDateTimePickerLegacy) c; else if ( c != null && c is TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse) { TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse ccc = (TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls.AccommodationMultiUseControl.CtrlAccommodationMultiUse) c; return ccc.GetDateTimePicker(); } else return null; } \n");
			stringBuilder.Append("  public Panel FindPanel( string controlCaption ) { Control c = FindControl( controlCaption ); if ( c != null && c is Panel ) return (Panel) c; else return null; } \n");
			stringBuilder.Append("  public RadioButton FindRadioButton( string controlCaption ) { Control c = FindControl( controlCaption ); if ( c != null && c is RadioButton ) return (RadioButton) c; else return null; } \n");
			return stringBuilder.ToString();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003760 File Offset: 0x00001960
		public static Compiler SetupNewCompiler2(Dictionary<int, Compiler> compilersArchive, int screenNum, string code_formLoaded, string code_preSave, string code_misc, object p_data, object da, object tripleDES, object student, Dictionary<string, object> args, string BinPath)
		{
			return Compiler.SetupNewCompiler(compilersArchive, screenNum, code_formLoaded, code_preSave, code_misc, p_data, da, (IEncryption)tripleDES, student, args, BinPath);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003790 File Offset: 0x00001990
		public static Compiler SetupNewCompiler(Dictionary<int, Compiler> compilersArchive, int screenNum, string code_formLoaded0, string code_preSave0, string code_misc0, object p_data, object da, IEncryption tripleDES, object student, Dictionary<string, object> args, string BinPath)
		{
			string text = (code_formLoaded0 ?? "").Trim();
			string text2 = (code_preSave0 ?? "").Trim();
			string text3 = (code_misc0 ?? "").Trim();
			bool flag = string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2) && string.IsNullOrEmpty(text3);
			Compiler result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Compiler compiler = null;
				bool flag2 = screenNum > 0 && compilersArchive.ContainsKey(screenNum);
				if (flag2)
				{
					compiler = compilersArchive[screenNum];
				}
				bool flag3 = compiler == null;
				if (flag3)
				{
					compiler = new Compiler(text, text2, text3, BinPath);
					bool flag4 = screenNum > 0;
					if (flag4)
					{
						compilersArchive.Add(screenNum, compiler);
					}
				}
				else
				{
					bool flag5 = compiler.NeedsRecompile(text, text2, text3);
					if (flag5)
					{
						bool flag6 = screenNum > 0;
						if (flag6)
						{
							compilersArchive.Remove(screenNum);
						}
						compiler = new Compiler(text, text2, text3, BinPath);
						bool flag7 = screenNum > 0;
						if (flag7)
						{
							compilersArchive.Add(screenNum, compiler);
						}
					}
				}
				compiler.Init(da, tripleDES, student, p_data, args);
				compiler.FormLoaded();
				result = compiler;
			}
			return result;
		}

		// Token: 0x0400002A RID: 42
		private readonly object _compiled;

		// Token: 0x0400002B RID: 43
		private readonly string _codeFormLoaded;

		// Token: 0x0400002C RID: 44
		private readonly string _codePreSave;

		// Token: 0x0400002D RID: 45
		private readonly string _codeMisc;
	}
}
