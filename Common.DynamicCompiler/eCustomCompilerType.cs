using System;

namespace TechnoPro.Common.DynamicCompiler
{
	// Token: 0x0200000A RID: 10
	public enum eCustomCompilerType
	{
		// Token: 0x04000022 RID: 34
		[CustomCompilerType(DefaultImports = new string[]
		{
			"System.dll",
			"System.Data.dll",
			"System.Xml.dll",
			"System.Windows.Forms.dll",
			"EncryptionClassLibrary.dll",
			"Common.DynamicCompiler.dll",
			"ClockWorkLogger.dll",
			"System.Core.dll",
			"Common.Reports.ICore.dll",
			"Common.Reports.Core.dll",
			"Common.Reports.Mappers.dll",
			"Common.Reports.Public.dll",
			"System.Drawing.dll"
		}, DefaultUsings = new string[]
		{
			"System",
			"System.Data",
			"TechnoPro.Common.DynamicCompiler.CompilerArgs.Reports",
			"TechnoPro.Common.DynamicCompiler.CompilerArgs",
			"System.Windows.Forms",
			"ClockWorkLogger",
			"System.Collections.Generic",
			"System.Linq",
			"System.Drawing"
		}, ConstructorCode = "_args = args;\r\n\t\t\t\r\nvar t2 = new DataTable(\"t2\");\r\ntry \r\n{\r\n\tt2.Columns.Add(\"variablename\");\r\n\tt2.Columns.Add(\"variablevalue\");\r\n\tt2.Columns.Add(\"datatype\");\r\n\tforeach ( ReportVariable v in _variables )\r\n\t\t\t\t\tt2.Rows.Add(new object[] { v.Name, v.Value == null ? \"NULL\" : v.Value.ToString(), v.Value == null ? \"?\" : v.Value.GetType().ToString() });\r\n    _variables.Add( new ReportVariable( \"status\", \"Complete!\") );\r\n\t\t\t\t\r\n\tLogMessage(\"Report complete.\");\r\n}\r\ncatch ( Exception ex )\r\n{\r\n    LogError(\"Report failed:{0}\", ex );\r\n    t2 = new DataTable(\"t2\");\r\n    t2.Columns.Add( \"err\" );\r\n    t2.Rows.Add( new object[] { ex.ToString() } );\r\n}\r\n\t\t\t\r\nreturn new ReportReturnValue() { Table = t2, VariablesOut = _variables };", PropertiesCode = "#region properties\r\n\t\t\r\nprivate ReportParameters _args;\r\nprivate DataTable _t { get { return _args == null ? null : _args.Table; } }\r\nprivate IList<ReportVariable> _variables { get { return _args == null || _args.Variables == null ? new List<ReportVariable>() : _args.Variables; } }\r\nprivate CompileContext _context { get { return _args == null || _args.Context == null ? new CompileContext() : _args.Context; } }\r\nprivate string GetVariable( params string[] varNames )\r\n{\r\n    if ( _args == null || _args.Variables == null ) return string.Empty;\r\n    foreach ( var varName in varNames )\r\n    {\r\n        var item = _args.Variables.FirstOrDefault( g => g.Name.Equals( varName, StringComparison.OrdinalIgnoreCase ) );\r\n        if ( item != null && item.Value != null )\r\n        {\r\n            var val = item.Value.ToString().Trim();\r\n            if ( val.Length > 0 ) return val;\r\n        }\r\n    }\r\n\r\n    //try the table if nothing was found\r\n    if ( _t == null || _t.Rows.Count < 1 ) return string.Empty;\r\n    var matchingColName = varNames.FirstOrDefault( g => _t.Columns.Contains( g ) );\r\n    if ( ! string.IsNullOrEmpty( matchingColName ) )\r\n        return _t.Rows[0][matchingColName].ToString().Trim();\r\n    return string.Empty;\r\n}\r\n\r\nprivate string GetStudentNumber() { return GetVariable( \"student_no\", \"studentno\" ).ToUpper(); }\r\nprivate string GetUsername() { return GetVariable( \"username\" ); }\r\nprivate void LogError( string title, Exception ex ) { ClockWorkLogger.CWLogger.Logger.Error( title, ex.ToString() ); }\r\nprivate void LogWarning( string msg ) { ClockWorkLogger.CWLogger.Logger.Warn( msg ); }\r\nprivate void LogMessage( string msg ) { ClockWorkLogger.CWLogger.Logger.Debug(msg); }\r\n\r\n//#if NET40\r\nprivate ReportReturnValue GetReturnValue( DataTable t ) { return new ReportReturnValue() { Table = t, VariablesOut = _variables }; }\r\nprivate TechnoPro.Common.Reports.Public.Entities.OperationContexts.OperationContextRO _opContext;\r\nprivate TechnoPro.Common.Reports.Public.Entities.OperationContexts.OperationContextRO OpContext { get { if ( _opContext != null ) return _opContext; _opContext = new TechnoPro.Common.Reports.Public.Entities.OperationContexts.OperationContextRO() { WhoAmI = _context == null ? 0 : _context.PersonId }; return _opContext; } }\r\nprivate string GetCustomSettingValue( TechnoPro.Common.Reports.Public.Entities.WebSettings.eWebCustomSetting customSettingCode )\r\n{\r\n    TechnoPro.Common.Reports.ICore.WebSettings.IWebSettingReportManager wrm = new TechnoPro.Common.Reports.Core.WebSettings.WebSettingReportManager(OpContext);\r\n    return wrm.GetCustomWebSettingValue(customSettingCode);\r\n}\r\n//#endif\r\n\t\t\r\n#endregion")]
		Reports,
		// Token: 0x04000023 RID: 35
		[CustomCompilerType(DefaultImports = new string[]
		{
			"system.dll",
			"system.data.dll",
			"system.xml.dll",
			"System.Windows.Forms.dll",
			"System.Drawing.dll",
			"Common.UI.WinForms.dll",
			"Common.UI.WinForms.Entity.dll",
			"DynamicScreens.dll",
			"AutoComboBox.dll",
			"EncryptionClassLibrary.dll",
			"Common.DynamicCompiler.dll"
		}, DefaultUsings = new string[]
		{
			"System.Windows.Forms",
			"System.Drawing",
			"System.Linq",
			"System",
			"System.Data",
			"TechnoPro.Common.UI.WinForms.DynamicFormsControls.Controls"
		})]
		DynamicForms,
		// Token: 0x04000024 RID: 36
		[CustomCompilerType(DefaultImports = new string[]
		{
			"system.dll",
			"system.data.dll",
			"system.xml.dll",
			"EncryptionClassLibrary.dll",
			"ClockWorkLogger.dll",
			"System.Core.dll"
		}, DefaultUsings = new string[]
		{
			"System",
			"System.Data",
			"ClockWorkLogger",
			"System.Collections.Generic",
			"System.Linq",
			"TechnoPro.Common.DynamicCompiler.CompilerArgs.MagneticCard",
			"TechnoPro.Common.DynamicCompiler.CompilerArgs"
		})]
		MagneticCard,
		// Token: 0x04000025 RID: 37
		[CustomCompilerType(DefaultImports = new string[]
		{
			"system.dll",
			"system.data.dll",
			"system.xml.dll",
			"EncryptionClassLibrary.dll",
			"ClockWorkLogger.dll",
			"System.Core.dll",
			"Common.Core.AuthenticationAuthorization.dll"
		}, DefaultUsings = new string[]
		{
			"System",
			"System.Data",
			"ClockWorkLogger",
			"System.Collections.Generic",
			"System.Linq",
			"TechnoPro.Common.Public",
			"TechnoPro.Common.Core.AuthenticationAuthorization",
			"TechnoPro.Common.DynamicCompiler.CompilerArgs"
		}, ConstructorCode = "var authenticationContext = (AuthenticationContext) args.AuthenticationContext;\r\nvar authorizationContext = (AuthorizationContext) args.AuthorizationContext;\r\nvar userName = args.UserName;\r\nvar password = args.Password;\r\nvar authenticationArgs = args.AuthenticationArgs;\r\n\r\nvar newAuthenticationArgs = new Dictionary<string,string>();\r\nforeach ( var aa in authenticationArgs )\r\n    if ( ! newAuthenticationArgs.ContainsKey( aa.Key ) )\r\n        newAuthenticationArgs.Add( aa.Key, aa.Value );\r\n\r\nreturn new AuthenticationCompilerReturnValue()\r\n{\r\n    AuthenticationContext = authenticationContext,\r\n    AuthorizationContext = authorizationContext,\r\n    UserName = userName,\r\n    Password = password,\r\n    AuthenticationArgs = newAuthenticationArgs\r\n};")]
		Authentication
	}
}
