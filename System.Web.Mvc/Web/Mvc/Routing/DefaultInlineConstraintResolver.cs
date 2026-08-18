using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc.Properties;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x02000027 RID: 39
	public class DefaultInlineConstraintResolver : IInlineConstraintResolver
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004512 File Offset: 0x00002712
		public IDictionary<string, Type> ConstraintMap
		{
			get
			{
				return this._inlineConstraintMap;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000451C File Offset: 0x0000271C
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

		// Token: 0x060000BE RID: 190 RVA: 0x00004688 File Offset: 0x00002888
		public virtual IRouteConstraint ResolveConstraint(string inlineConstraint)
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
			if (!typeof(IRouteConstraint).IsAssignableFrom(type))
			{
				throw Error.InvalidOperation(MvcResources.DefaultInlineConstraintResolver_TypeNotConstraint, new object[]
				{
					type.Name,
					text
				});
			}
			return (IRouteConstraint)DefaultInlineConstraintResolver.CreateConstraint(type, argumentString);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000475C File Offset: 0x0000295C
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
					throw Error.InvalidOperation(MvcResources.DefaultInlineConstraintResolver_CouldNotFindCtor, new object[]
					{
						constraintType.Name,
						argumentString.Length
					});
				}
				if (num != 1)
				{
					throw Error.InvalidOperation(MvcResources.DefaultInlineConstraintResolver_AmbiguousCtors, new object[]
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

		// Token: 0x060000C0 RID: 192 RVA: 0x000048A4 File Offset: 0x00002AA4
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

		// Token: 0x0400002E RID: 46
		private readonly IDictionary<string, Type> _inlineConstraintMap = DefaultInlineConstraintResolver.GetDefaultConstraintMap();
	}
}
