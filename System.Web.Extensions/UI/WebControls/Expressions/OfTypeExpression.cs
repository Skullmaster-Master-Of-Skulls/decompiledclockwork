using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Resources;

namespace System.Web.UI.WebControls.Expressions
{
	// Token: 0x020000CF RID: 207
	public class OfTypeExpression : DataSourceExpression
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00026820 File Offset: 0x00024A20
		private MethodInfo OfTypeMethod
		{
			get
			{
				if (this._ofTypeMethod == null)
				{
					Type type = this.GetType(this.TypeName);
					this._ofTypeMethod = OfTypeExpression.GetOfTypeMethod(type);
				}
				return this._ofTypeMethod;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0002685A File Offset: 0x00024A5A
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x0002686B File Offset: 0x00024A6B
		[DefaultValue("")]
		public string TypeName
		{
			get
			{
				return this._typeName ?? string.Empty;
			}
			set
			{
				if (this.TypeName != value)
				{
					this._typeName = value;
					this._ofTypeMethod = null;
				}
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00026889 File Offset: 0x00024A89
		public OfTypeExpression()
		{
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00026891 File Offset: 0x00024A91
		public OfTypeExpression(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.TypeName = type.AssemblyQualifiedName;
			this._ofTypeMethod = OfTypeExpression.GetOfTypeMethod(type);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000268C5 File Offset: 0x00024AC5
		internal OfTypeExpression(Control owner) : base(owner)
		{
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000268D0 File Offset: 0x00024AD0
		private Type GetType(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.OfTypeExpression_TypeNameNotSpecified, new object[]
				{
					base.Owner.ID
				}));
			}
			Type type;
			try
			{
				type = BuildManager.GetType(typeName, true, true);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.OfTypeExpression_CannotFindType, new object[]
				{
					typeName,
					base.Owner.ID
				}), innerException);
			}
			return type;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002695C File Offset: 0x00024B5C
		private static MethodInfo GetOfTypeMethod(Type type)
		{
			return typeof(Queryable).GetMethod("OfType").MakeGenericMethod(new Type[]
			{
				type
			});
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00026981 File Offset: 0x00024B81
		public override IQueryable GetQueryable(IQueryable query)
		{
			return query.Provider.CreateQuery(Expression.Call(null, this.OfTypeMethod, new Expression[]
			{
				query.Expression
			}));
		}

		// Token: 0x04000350 RID: 848
		private MethodInfo _ofTypeMethod;

		// Token: 0x04000351 RID: 849
		private string _typeName;
	}
}
