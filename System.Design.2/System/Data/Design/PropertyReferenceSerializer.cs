using System;
using System.CodeDom;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;

namespace System.Data.Design
{
	// Token: 0x02000257 RID: 599
	internal class PropertyReferenceSerializer
	{
		// Token: 0x060016EF RID: 5871 RVA: 0x0000362F File Offset: 0x0000182F
		private PropertyReferenceSerializer()
		{
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0007DA47 File Offset: 0x0007BC47
		internal static string Serialize(CodePropertyReferenceExpression expression)
		{
			if (PropertyReferenceSerializer.IsWellKnownApplicationSettingsExpression(expression))
			{
				return PropertyReferenceSerializer.SerializeApplicationSettingsExpression(expression);
			}
			if (PropertyReferenceSerializer.IsWellKnownAppConfigExpression(expression))
			{
				return PropertyReferenceSerializer.SerializeAppConfigExpression(expression);
			}
			return PropertyReferenceSerializer.SerializeWithSoapFormatter(expression);
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0007DA70 File Offset: 0x0007BC70
		internal static CodePropertyReferenceExpression Deserialize(string expressionString)
		{
			string[] array = expressionString.Split(new char[]
			{
				'.'
			});
			if (array != null && array.Length != 0)
			{
				if (StringUtil.EqualValue(array[0], "ApplicationSettings"))
				{
					return PropertyReferenceSerializer.DeserializeApplicationSettingsExpression(array);
				}
				if (StringUtil.EqualValue(array[0], "AppConfig"))
				{
					return PropertyReferenceSerializer.DeserializeAppConfigExpression(array);
				}
			}
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			MemoryStream serializationStream = new MemoryStream(utf8Encoding.GetBytes(expressionString));
			IFormatter formatter = new SoapFormatter();
			return (CodePropertyReferenceExpression)formatter.Deserialize(serializationStream);
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0007DAE8 File Offset: 0x0007BCE8
		private static string SerializeWithSoapFormatter(CodePropertyReferenceExpression expression)
		{
			MemoryStream memoryStream = new MemoryStream();
			IFormatter formatter = new SoapFormatter();
			formatter.Serialize(memoryStream, expression);
			if (memoryStream.Length > 2147483647L)
			{
				throw new InternalException("Serialized property expression is too long.");
			}
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			byte[] array = new byte[memoryStream.Length];
			memoryStream.Position = 0L;
			memoryStream.Read(array, 0, (int)memoryStream.Length);
			return utf8Encoding.GetString(array);
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0007DB54 File Offset: 0x0007BD54
		private static string SerializeApplicationSettingsExpression(CodePropertyReferenceExpression expression)
		{
			string text = expression.PropertyName;
			CodePropertyReferenceExpression codePropertyReferenceExpression = (CodePropertyReferenceExpression)expression.TargetObject;
			text = codePropertyReferenceExpression.PropertyName + "." + text;
			CodeTypeReferenceExpression codeTypeReferenceExpression = (CodeTypeReferenceExpression)codePropertyReferenceExpression.TargetObject;
			text = codeTypeReferenceExpression.Type.Options.ToString() + "." + text;
			text = codeTypeReferenceExpression.Type.BaseType + "." + text;
			return "ApplicationSettings." + text;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0007DBDC File Offset: 0x0007BDDC
		private static string SerializeAppConfigExpression(CodePropertyReferenceExpression expression)
		{
			string text = expression.PropertyName;
			CodeIndexerExpression codeIndexerExpression = (CodeIndexerExpression)expression.TargetObject;
			string str = ((CodePrimitiveExpression)codeIndexerExpression.Indices[0]).Value as string;
			text = str + "." + text;
			CodePropertyReferenceExpression codePropertyReferenceExpression = (CodePropertyReferenceExpression)codeIndexerExpression.TargetObject;
			text = codePropertyReferenceExpression.PropertyName + "." + text;
			CodeTypeReferenceExpression codeTypeReferenceExpression = (CodeTypeReferenceExpression)codePropertyReferenceExpression.TargetObject;
			text = codeTypeReferenceExpression.Type.Options.ToString() + "." + text;
			text = codeTypeReferenceExpression.Type.BaseType + "." + text;
			return "AppConfig." + text;
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0007DC9C File Offset: 0x0007BE9C
		private static bool IsWellKnownApplicationSettingsExpression(CodePropertyReferenceExpression expression)
		{
			if (expression.UserData != null && expression.UserData.Count > 0)
			{
				return false;
			}
			if (!(expression.TargetObject is CodePropertyReferenceExpression))
			{
				return false;
			}
			CodePropertyReferenceExpression codePropertyReferenceExpression = (CodePropertyReferenceExpression)expression.TargetObject;
			if (codePropertyReferenceExpression.UserData != null && codePropertyReferenceExpression.UserData.Count > 0)
			{
				return false;
			}
			if (!(codePropertyReferenceExpression.TargetObject is CodeTypeReferenceExpression))
			{
				return false;
			}
			CodeTypeReferenceExpression codeTypeReferenceExpression = (CodeTypeReferenceExpression)codePropertyReferenceExpression.TargetObject;
			if (codeTypeReferenceExpression.UserData != null && codeTypeReferenceExpression.UserData.Count > 0)
			{
				return false;
			}
			CodeTypeReference type = codeTypeReferenceExpression.Type;
			return (type.UserData == null || type.UserData.Count <= 0) && (type.TypeArguments == null || type.TypeArguments.Count <= 0) && type.ArrayElementType == null && type.ArrayRank <= 0;
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0007DD74 File Offset: 0x0007BF74
		private static bool IsWellKnownAppConfigExpression(CodePropertyReferenceExpression expression)
		{
			if (expression.UserData != null && expression.UserData.Count > 0)
			{
				return false;
			}
			if (!(expression.TargetObject is CodeIndexerExpression))
			{
				return false;
			}
			CodeIndexerExpression codeIndexerExpression = (CodeIndexerExpression)expression.TargetObject;
			if (codeIndexerExpression.UserData != null && codeIndexerExpression.UserData.Count > 0)
			{
				return false;
			}
			if (codeIndexerExpression.Indices == null || codeIndexerExpression.Indices.Count != 1 || !(codeIndexerExpression.Indices[0] is CodePrimitiveExpression))
			{
				return false;
			}
			if (!(((CodePrimitiveExpression)codeIndexerExpression.Indices[0]).Value is string))
			{
				return false;
			}
			if (!(codeIndexerExpression.TargetObject is CodePropertyReferenceExpression))
			{
				return false;
			}
			CodePropertyReferenceExpression codePropertyReferenceExpression = (CodePropertyReferenceExpression)codeIndexerExpression.TargetObject;
			if (codePropertyReferenceExpression.UserData != null && codePropertyReferenceExpression.UserData.Count > 0)
			{
				return false;
			}
			if (!(codePropertyReferenceExpression.TargetObject is CodeTypeReferenceExpression))
			{
				return false;
			}
			CodeTypeReferenceExpression codeTypeReferenceExpression = (CodeTypeReferenceExpression)codePropertyReferenceExpression.TargetObject;
			if (codeTypeReferenceExpression.UserData != null && codeTypeReferenceExpression.UserData.Count > 0)
			{
				return false;
			}
			CodeTypeReference type = codeTypeReferenceExpression.Type;
			return (type.UserData == null || type.UserData.Count <= 0) && (type.TypeArguments == null || type.TypeArguments.Count <= 0) && type.ArrayElementType == null && type.ArrayRank <= 0;
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0007DEC8 File Offset: 0x0007C0C8
		private static CodePropertyReferenceExpression DeserializeApplicationSettingsExpression(string[] expressionParts)
		{
			int i = expressionParts.Length - 1;
			CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression();
			codePropertyReferenceExpression.PropertyName = expressionParts[i];
			i--;
			CodePropertyReferenceExpression codePropertyReferenceExpression2 = new CodePropertyReferenceExpression();
			codePropertyReferenceExpression.TargetObject = codePropertyReferenceExpression2;
			codePropertyReferenceExpression2.PropertyName = expressionParts[i];
			i--;
			CodeTypeReferenceExpression codeTypeReferenceExpression = new CodeTypeReferenceExpression();
			codePropertyReferenceExpression2.TargetObject = codeTypeReferenceExpression;
			codeTypeReferenceExpression.Type.Options = (CodeTypeReferenceOptions)Enum.Parse(typeof(CodeTypeReferenceOptions), expressionParts[i]);
			i--;
			codeTypeReferenceExpression.Type.BaseType = expressionParts[i];
			for (i--; i > 0; i--)
			{
				codeTypeReferenceExpression.Type.BaseType = expressionParts[i] + "." + codeTypeReferenceExpression.Type.BaseType;
			}
			return codePropertyReferenceExpression;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0007DF7C File Offset: 0x0007C17C
		private static CodePropertyReferenceExpression DeserializeAppConfigExpression(string[] expressionParts)
		{
			int i = expressionParts.Length - 1;
			CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression();
			codePropertyReferenceExpression.PropertyName = expressionParts[i];
			i--;
			CodeIndexerExpression codeIndexerExpression = new CodeIndexerExpression();
			codePropertyReferenceExpression.TargetObject = codeIndexerExpression;
			codeIndexerExpression.Indices.Add(new CodePrimitiveExpression(expressionParts[i]));
			i--;
			CodePropertyReferenceExpression codePropertyReferenceExpression2 = new CodePropertyReferenceExpression();
			codeIndexerExpression.TargetObject = codePropertyReferenceExpression2;
			codePropertyReferenceExpression2.PropertyName = expressionParts[i];
			i--;
			CodeTypeReferenceExpression codeTypeReferenceExpression = new CodeTypeReferenceExpression();
			codePropertyReferenceExpression2.TargetObject = codeTypeReferenceExpression;
			codeTypeReferenceExpression.Type.Options = (CodeTypeReferenceOptions)Enum.Parse(typeof(CodeTypeReferenceOptions), expressionParts[i]);
			i--;
			codeTypeReferenceExpression.Type.BaseType = expressionParts[i];
			for (i--; i > 0; i--)
			{
				codeTypeReferenceExpression.Type.BaseType = expressionParts[i] + "." + codeTypeReferenceExpression.Type.BaseType;
			}
			return codePropertyReferenceExpression;
		}

		// Token: 0x04000BB0 RID: 2992
		private const string applicationSettingsPrefix = "ApplicationSettings";

		// Token: 0x04000BB1 RID: 2993
		private const string appConfigPrefix = "AppConfig";
	}
}
