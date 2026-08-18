using System;
using System.CodeDom;
using System.Collections;
using System.Design;
using System.Globalization;

namespace System.Data.Design
{
	// Token: 0x02000243 RID: 579
	internal sealed class ExtendedPropertiesHandler
	{
		// Token: 0x0600168C RID: 5772 RVA: 0x0000362F File Offset: 0x0000182F
		private ExtendedPropertiesHandler()
		{
		}

		// Token: 0x17000532 RID: 1330
		// (set) Token: 0x0600168D RID: 5773 RVA: 0x0007C02F File Offset: 0x0007A22F
		internal static TypedDataSourceCodeGenerator CodeGenerator
		{
			set
			{
				ExtendedPropertiesHandler.codeGenerator = value;
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0007C038 File Offset: 0x0007A238
		internal static void AddExtendedProperties(DataSourceComponent targetObj, CodeExpression addTarget, IList statementCollection, Hashtable extendedProperties)
		{
			if (extendedProperties == null)
			{
				return;
			}
			if (addTarget == null)
			{
				throw new InternalException("ExtendedPropertiesHandler.AddExtendedProperties: addTarget cannot be null");
			}
			if (statementCollection == null)
			{
				throw new InternalException("ExtendedPropertiesHandler.AddExtendedProperties: statementCollection cannot be null");
			}
			if (ExtendedPropertiesHandler.codeGenerator == null)
			{
				throw new InternalException("ExtendedPropertiesHandler.AddExtendedProperties: codeGenerator cannot be null");
			}
			if (targetObj == null)
			{
				throw new InternalException("ExtendedPropertiesHandler.AddExtendedProperties: targetObject cannot be null");
			}
			ExtendedPropertiesHandler.targetObject = targetObj;
			if (ExtendedPropertiesHandler.codeGenerator.GenerateExtendedProperties)
			{
				ExtendedPropertiesHandler.GenerateProperties(addTarget, statementCollection, extendedProperties);
				return;
			}
			SortedList sortedList = new SortedList(new Comparer(CultureInfo.InvariantCulture));
			foreach (string key in ExtendedPropertiesHandler.targetObject.NamingPropertyNames)
			{
				string text = extendedProperties[key] as string;
				if (!StringUtil.Empty(text))
				{
					sortedList.Add(key, text);
				}
			}
			ExtendedPropertiesHandler.GenerateProperties(addTarget, statementCollection, sortedList);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0007C11C File Offset: 0x0007A31C
		private static void GenerateProperties(CodeExpression addTarget, IList statementCollection, ICollection extendedProperties)
		{
			if (extendedProperties != null)
			{
				IDictionaryEnumerator dictionaryEnumerator = (IDictionaryEnumerator)extendedProperties.GetEnumerator();
				if (dictionaryEnumerator != null)
				{
					dictionaryEnumerator.Reset();
					while (dictionaryEnumerator.MoveNext())
					{
						string text = dictionaryEnumerator.Key as string;
						string text2 = dictionaryEnumerator.Value as string;
						if (text == null || text2 == null)
						{
							ExtendedPropertiesHandler.codeGenerator.ProblemList.Add(new DSGeneratorProblem(SR.GetString("CG_UnableToReadExtProperties"), ProblemSeverity.NonFatalError, ExtendedPropertiesHandler.targetObject));
						}
						else
						{
							statementCollection.Add(CodeGenHelper.Stm(CodeGenHelper.MethodCall(CodeGenHelper.Property(addTarget, "ExtendedProperties"), "Add", new CodeExpression[]
							{
								CodeGenHelper.Primitive(text),
								CodeGenHelper.Primitive(text2)
							})));
						}
					}
				}
			}
		}

		// Token: 0x04000B91 RID: 2961
		private static TypedDataSourceCodeGenerator codeGenerator;

		// Token: 0x04000B92 RID: 2962
		private static DataSourceComponent targetObject;
	}
}
