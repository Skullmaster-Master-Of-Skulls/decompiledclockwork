using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Web.Http.Internal;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000121 RID: 289
	public class ReflectedHttpParameterDescriptor : HttpParameterDescriptor
	{
		// Token: 0x060006FF RID: 1791 RVA: 0x0001737C File Offset: 0x0001557C
		public ReflectedHttpParameterDescriptor(HttpActionDescriptor actionDescriptor, ParameterInfo parameterInfo) : base(actionDescriptor)
		{
			if (parameterInfo == null)
			{
				throw Error.ArgumentNull("parameterInfo");
			}
			this.ParameterInfo = parameterInfo;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001739A File Offset: 0x0001559A
		public ReflectedHttpParameterDescriptor()
		{
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x000173A4 File Offset: 0x000155A4
		public override object DefaultValue
		{
			get
			{
				object result;
				if (this.ParameterInfo.TryGetDefaultValue(out result))
				{
					return result;
				}
				return base.DefaultValue;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x000173C8 File Offset: 0x000155C8
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x000173D0 File Offset: 0x000155D0
		public ParameterInfo ParameterInfo
		{
			get
			{
				return this._parameterInfo;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._parameterInfo = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x000173E2 File Offset: 0x000155E2
		public override bool IsOptional
		{
			get
			{
				return this.ParameterInfo.IsOptional;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x000173EF File Offset: 0x000155EF
		public override string ParameterName
		{
			get
			{
				return this.ParameterInfo.Name;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x000173FC File Offset: 0x000155FC
		public override Type ParameterType
		{
			get
			{
				return this.ParameterInfo.ParameterType;
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00017409 File Offset: 0x00015609
		public override Collection<TAttribute> GetCustomAttributes<TAttribute>()
		{
			return new Collection<TAttribute>((TAttribute[])this.ParameterInfo.GetCustomAttributes(typeof(TAttribute), false));
		}

		// Token: 0x04000200 RID: 512
		private ParameterInfo _parameterInfo;
	}
}
