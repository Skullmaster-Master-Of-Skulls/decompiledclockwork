using System;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x0200001F RID: 31
	public class LengthRouteConstraint : IRouteConstraint
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00003FA7 File Offset: 0x000021A7
		public LengthRouteConstraint(int length)
		{
			if (length < 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("length", length, 0);
			}
			this.Length = new int?(length);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003FCC File Offset: 0x000021CC
		public LengthRouteConstraint(int minLength, int maxLength)
		{
			if (minLength < 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("minLength", minLength, 0);
			}
			if (maxLength < 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxLength", maxLength, 0);
			}
			this.MinLength = new int?(minLength);
			this.MaxLength = new int?(maxLength);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004019 File Offset: 0x00002219
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00004021 File Offset: 0x00002221
		public int? Length { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000402A File Offset: 0x0000222A
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00004032 File Offset: 0x00002232
		public int? MinLength { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000403B File Offset: 0x0000223B
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00004043 File Offset: 0x00002243
		public int? MaxLength { get; private set; }

		// Token: 0x0600009F RID: 159 RVA: 0x0000404C File Offset: 0x0000224C
		public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			if (parameterName == null)
			{
				throw Error.ArgumentNull("parameterName");
			}
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			object obj;
			if (!values.TryGetValue(parameterName, out obj) || obj == null)
			{
				return false;
			}
			string text = Convert.ToString(obj, CultureInfo.InvariantCulture);
			int length = text.Length;
			if (this.Length != null)
			{
				return length == this.Length.Value;
			}
			return length >= this.MinLength.Value && length <= this.MaxLength.Value;
		}
	}
}
