using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Http.Properties;
using System.Web.Http.Routing.Constraints;

namespace System.Web.Http.Routing
{
	// Token: 0x0200009A RID: 154
	public class DefaultInlineConstraintResolver : IInlineConstraintResolver
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000B7C2 File Offset: 0x000099C2
		public IDictionary<string, Type> ConstraintMap
		{
			get
			{
				return this._inlineConstraintMap;
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000B7CC File Offset: 0x000099CC
		private static IDictionary<string, Type> GetDefaultConstraintMap()
		{
			return new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
			{
				{
					"bool",
					typeof(BoolRouteConstraint)
				},
				{
					"datetime",
					typeof(DateTimeRouteConstraint)
				},
				{
					"decimal",
					typeof(DecimalRouteConstraint)
				},
				{
					"double",
					typeof(DoubleRouteConstraint)
				},
				{
					"float",
					typeof(FloatRouteConstraint)
				},
				{
					"guid",
					typeof(GuidRouteConstraint)
				},
				{
					"int",
					typeof(IntRouteConstraint)
				},
				{
					"long",
					typeof(LongRouteConstraint)
				},
				{
					"minlength",
					typeof(MinLengthRouteConstraint)
				},
				{
					"maxlength",
					typeof(MaxLengthRouteConstraint)
				},
				{
					"length",
					typeof(LengthRouteConstraint)
				},
				{
					"min",
					typeof(MinRouteConstraint)
				},
				{
					"max",
					typeof(MaxRouteConstraint)
				},
				{
					"range",
					typeof(RangeRouteConstraint)
				},
				{
					"alpha",
					typeof(AlphaRouteConstraint)
				},
				{
					"regex",
					typeof(RegexRouteConstraint)
				}
			};
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000B938 File Offset: 0x00009B38
		public virtual IHttpRouteConstraint ResolveConstraint(string inlineConstraint)
		{
			if (inlineConstraint == null)
			{
				throw Error.ArgumentNull("inlineConstraint");
			}
			int num = inlineConstraint.IndexOf('(');
			string text;
			string argumentString;
			if (num >= 0 && inlineConstraint.EndsWith(")", StringComparison.Ordinal))
			{
				text = inlineConstraint.Substring(0, num);
				argumentString = inlineConstraint.Substring(num + 1, inlineConstraint.Length - num - 2);
			}
			else
			{
				text = inlineConstraint;
				argumentString = null;
			}
			Type type;
			if (!this._inlineConstraintMap.TryGetValue(text, out type))
			{
				return null;
			}
			if (!typeof(IHttpRouteConstraint).IsAssignableFrom(type))
			{
				throw Error.InvalidOperation(SRResources.DefaultInlineConstraintResolver_TypeNotConstraint, new object[]
				{
					type.Name,
					text
				});
			}
			return (IHttpRouteConstraint)DefaultInlineConstraintResolver.CreateConstraint(type, argumentString);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000BA0C File Offset: 0x00009C0C
		private static object CreateConstraint(Type constraintType, string argumentString)
		{
			if (argumentString == null)
			{
				return Activator.CreateInstance(constraintType);
			}
			ConstructorInfo[] constructors = constraintType.GetConstructors();
			ConstructorInfo constructorInfo;
			object[] parameters;
			if (constructors.Length == 1 && constructors[0].GetParameters().Length == 1)
			{
				constructorInfo = constructors[0];
				parameters = DefaultInlineConstraintResolver.ConvertArguments(constructorInfo.GetParameters(), new string[]
				{
					argumentString
				});
			}
			else
			{
				string[] arguments = (from argument in argumentString.Split(new char[]
				{
					','
				})
				select argument.Trim()).ToArray<string>();
				ConstructorInfo[] array = (from ci in constructors
				where ci.GetParameters().Length == arguments.Length
				select ci).ToArray<ConstructorInfo>();
				int num = array.Length;
				if (num == 0)
				{
					throw Error.InvalidOperation(SRResources.DefaultInlineConstraintResolver_CouldNotFindCtor, new object[]
					{
						constraintType.Name,
						argumentString.Length
					});
				}
				if (num != 1)
				{
					throw Error.InvalidOperation(SRResources.DefaultInlineConstraintResolver_AmbiguousCtors, new object[]
					{
						constraintType.Name,
						argumentString.Length
					});
				}
				constructorInfo = array[0];
				parameters = DefaultInlineConstraintResolver.ConvertArguments(constructorInfo.GetParameters(), arguments);
			}
			return constructorInfo.Invoke(parameters);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000BB54 File Offset: 0x00009D54
		private static object[] ConvertArguments(ParameterInfo[] parameterInfos, string[] arguments)
		{
			object[] array = new object[parameterInfos.Length];
			for (int i = 0; i < parameterInfos.Length; i++)
			{
				ParameterInfo parameterInfo = parameterInfos[i];
				Type parameterType = parameterInfo.ParameterType;
				array[i] = Convert.ChangeType(arguments[i], parameterType, CultureInfo.InvariantCulture);
			}
			return array;
		}

		// Token: 0x04000110 RID: 272
		private readonly IDictionary<string, Type> _inlineConstraintMap = DefaultInlineConstraintResolver.GetDefaultConstraintMap();
	}
}
