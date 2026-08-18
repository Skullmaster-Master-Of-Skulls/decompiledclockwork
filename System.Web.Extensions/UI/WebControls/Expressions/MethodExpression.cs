using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.DynamicData;
using System.Web.Resources;

namespace System.Web.UI.WebControls.Expressions
{
	// Token: 0x020000CE RID: 206
	public class MethodExpression : ParameterDataSourceExpression
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0002642C File Offset: 0x0002462C
		// (set) Token: 0x06000A37 RID: 2615 RVA: 0x0002644C File Offset: 0x0002464C
		public string TypeName
		{
			get
			{
				return ((string)base.ViewState["TypeName"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["TypeName"] = value;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0002645F File Offset: 0x0002465F
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x0002647F File Offset: 0x0002467F
		public string MethodName
		{
			get
			{
				return ((string)base.ViewState["MethodName"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["MethodName"] = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00026494 File Offset: 0x00024694
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x000264BD File Offset: 0x000246BD
		public bool IgnoreIfNotFound
		{
			get
			{
				object obj = base.ViewState["IgnoreIfNotFound"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["IgnoreIfNotFound"] = value;
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000264D8 File Offset: 0x000246D8
		public MethodExpression()
		{
			this.typeGetters = new Func<Type>[]
			{
				() => MethodExpression.GetType(this.TypeName),
				() => MethodExpression.GetType(base.DataSource),
				delegate()
				{
					if (base.Owner == null || base.Owner.TemplateControl == null)
					{
						return null;
					}
					return base.Owner.TemplateControl.GetType();
				}
			};
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00026524 File Offset: 0x00024724
		private static Type GetType(string typeName)
		{
			if (!string.IsNullOrEmpty(typeName))
			{
				return BuildManager.GetType(typeName, false, true);
			}
			return null;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00026538 File Offset: 0x00024738
		private static Type GetType(IQueryableDataSource dataSource)
		{
			IDynamicDataSource dynamicDataSource = dataSource as IDynamicDataSource;
			if (dynamicDataSource != null)
			{
				return dynamicDataSource.ContextType;
			}
			return null;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00026558 File Offset: 0x00024758
		internal MethodInfo ResolveMethod()
		{
			if (string.IsNullOrEmpty(this.MethodName))
			{
				throw new InvalidOperationException(AtlasWeb.MethodExpression_MethodNameMustBeSpecified);
			}
			MethodInfo methodInfo = null;
			IDynamicDataSource dynamicDataSource = base.DataSource as IDynamicDataSource;
			if (dynamicDataSource != null)
			{
				this.MethodName = string.Format(CultureInfo.CurrentCulture, this.MethodName, new object[]
				{
					dynamicDataSource.EntitySetName
				});
			}
			else if (this.MethodName.Contains("{0}"))
			{
				throw new InvalidOperationException(AtlasWeb.MethodExpression_DataSourceMustBeIDynamicDataSource);
			}
			foreach (Func<Type> func in this.typeGetters)
			{
				Type type = func();
				if (!(type == null))
				{
					methodInfo = type.GetMethod(this.MethodName, MethodExpression.MethodFlags);
					if (methodInfo != null)
					{
						break;
					}
				}
			}
			return methodInfo;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002661C File Offset: 0x0002481C
		public override IQueryable GetQueryable(IQueryable source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			MethodInfo methodInfo = this.ResolveMethod();
			IDictionary<string, object> values = this.GetValues();
			if (methodInfo == null)
			{
				if (this.IgnoreIfNotFound)
				{
					return source;
				}
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.MethodExpression_MethodNotFound, new object[]
				{
					this.MethodName
				}));
			}
			else
			{
				if (!methodInfo.IsStatic)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.MethodExpression_MethodMustBeStatic, new object[]
					{
						this.MethodName
					}));
				}
				ParameterInfo[] parameters = methodInfo.GetParameters();
				if (parameters.Length == 0 || !parameters[0].ParameterType.IsAssignableFrom(source.GetType()))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.MethodExpression_FirstParamterMustBeCorrectType, new object[]
					{
						this.MethodName,
						source.GetType()
					}));
				}
				object[] array = new object[parameters.Length];
				array[0] = source;
				for (int i = 1; i < parameters.Length; i++)
				{
					ParameterInfo parameterInfo = parameters[i];
					object value;
					if (!values.TryGetValue(parameterInfo.Name, out value))
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.MethodExpression_ParameterNotFound, new object[]
						{
							this.MethodName,
							parameterInfo.Name
						}));
					}
					array[i] = DataSourceHelper.BuildObjectValue(value, parameterInfo.ParameterType, parameterInfo.Name);
				}
				object obj = methodInfo.Invoke(null, array);
				if (obj != null)
				{
					IQueryable queryable = obj as IQueryable;
					if (queryable == null || !queryable.ElementType.IsAssignableFrom(source.ElementType))
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.MethodExpression_ChangingTheReturnTypeIsNotAllowed, new object[]
						{
							source.ElementType.FullName
						}));
					}
				}
				return (IQueryable)obj;
			}
		}

		// Token: 0x0400034E RID: 846
		private static readonly BindingFlags MethodFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;

		// Token: 0x0400034F RID: 847
		private Func<Type>[] typeGetters;
	}
}
