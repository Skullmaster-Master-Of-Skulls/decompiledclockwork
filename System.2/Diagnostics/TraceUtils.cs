using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace System.Diagnostics
{
	// Token: 0x020004B9 RID: 1209
	internal static class TraceUtils
	{
		// Token: 0x06002D33 RID: 11571 RVA: 0x000CB6E8 File Offset: 0x000C98E8
		internal static object GetRuntimeObject(string className, Type baseType, string initializeData)
		{
			object obj = null;
			if (className.Length == 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("EmptyTypeName_NotAllowed"));
			}
			Type type = Type.GetType(className);
			if (type == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Could_not_find_type", new object[]
				{
					className
				}));
			}
			if (!baseType.IsAssignableFrom(type))
			{
				throw new ConfigurationErrorsException(SR.GetString("Incorrect_base_type", new object[]
				{
					className,
					baseType.FullName
				}));
			}
			Exception ex = null;
			try
			{
				if (string.IsNullOrEmpty(initializeData))
				{
					if (TraceUtils.IsOwnedTL(type))
					{
						throw new ConfigurationErrorsException(SR.GetString("TL_InitializeData_NotSpecified"));
					}
					ConstructorInfo constructor = type.GetConstructor(new Type[0]);
					if (constructor == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Could_not_get_constructor", new object[]
						{
							className
						}));
					}
					obj = SecurityUtils.ConstructorInfoInvoke(constructor, new object[0]);
				}
				else
				{
					ConstructorInfo constructor2 = type.GetConstructor(new Type[]
					{
						typeof(string)
					});
					if (constructor2 != null)
					{
						if (TraceUtils.IsOwnedTextWriterTL(type) && initializeData[0] != Path.DirectorySeparatorChar && initializeData[0] != Path.AltDirectorySeparatorChar && !Path.IsPathRooted(initializeData))
						{
							string configFilePath = DiagnosticsConfiguration.ConfigFilePath;
							if (!string.IsNullOrEmpty(configFilePath))
							{
								string directoryName = Path.GetDirectoryName(configFilePath);
								if (directoryName != null)
								{
									initializeData = Path.Combine(directoryName, initializeData);
								}
							}
						}
						obj = SecurityUtils.ConstructorInfoInvoke(constructor2, new object[]
						{
							initializeData
						});
					}
					else
					{
						ConstructorInfo[] constructors = type.GetConstructors();
						if (constructors == null)
						{
							throw new ConfigurationErrorsException(SR.GetString("Could_not_get_constructor", new object[]
							{
								className
							}));
						}
						for (int i = 0; i < constructors.Length; i++)
						{
							ParameterInfo[] parameters = constructors[i].GetParameters();
							if (parameters.Length == 1)
							{
								Type parameterType = parameters[0].ParameterType;
								try
								{
									object obj2 = TraceUtils.ConvertToBaseTypeOrEnum(initializeData, parameterType);
									obj = SecurityUtils.ConstructorInfoInvoke(constructors[i], new object[]
									{
										obj2
									});
									break;
								}
								catch (TargetInvocationException ex2)
								{
									ex = ex2.InnerException;
								}
								catch (Exception ex3)
								{
									ex = ex3;
								}
							}
						}
					}
				}
			}
			catch (TargetInvocationException ex4)
			{
				ex = ex4.InnerException;
			}
			if (obj != null)
			{
				return obj;
			}
			if (ex != null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Could_not_create_type_instance", new object[]
				{
					className
				}), ex);
			}
			throw new ConfigurationErrorsException(SR.GetString("Could_not_create_type_instance", new object[]
			{
				className
			}));
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000CB97C File Offset: 0x000C9B7C
		internal static bool IsOwnedTL(Type type)
		{
			return typeof(EventLogTraceListener) == type || TraceUtils.IsOwnedTextWriterTL(type);
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000CB998 File Offset: 0x000C9B98
		internal static bool IsOwnedTextWriterTL(Type type)
		{
			return typeof(XmlWriterTraceListener) == type || typeof(DelimitedListTraceListener) == type || typeof(TextWriterTraceListener) == type;
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000CB9D0 File Offset: 0x000C9BD0
		private static object ConvertToBaseTypeOrEnum(string value, Type type)
		{
			if (type.IsEnum)
			{
				return Enum.Parse(type, value, false);
			}
			return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x000CB9F0 File Offset: 0x000C9BF0
		internal static void VerifyAttributes(IDictionary attributes, string[] supportedAttributes, object parent)
		{
			foreach (object obj in attributes.Keys)
			{
				string text = (string)obj;
				bool flag = false;
				if (supportedAttributes != null)
				{
					for (int i = 0; i < supportedAttributes.Length; i++)
					{
						if (supportedAttributes[i] == text)
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					throw new ConfigurationErrorsException(SR.GetString("AttributeNotSupported", new object[]
					{
						text,
						parent.GetType().FullName
					}));
				}
			}
		}
	}
}
