using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000092 RID: 146
	public class LengthRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x0600038E RID: 910 RVA: 0x0000B21F File Offset: 0x0000941F
		public LengthRouteConstraint(int length)
		{
			if (length < 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("length", length, 0);
			}
			this.Length = new int?(length);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000B250 File Offset: 0x00009450
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

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000B2B1 File Offset: 0x000094B1
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0000B2B9 File Offset: 0x000094B9
		public int? Length { get; private set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000B2C2 File Offset: 0x000094C2
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0000B2CA File Offset: 0x000094CA
		public int? MinLength { get; private set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000B2D3 File Offset: 0x000094D3
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000B2DB File Offset: 0x000094DB
		public int? MaxLength { get; private set; }

		// Token: 0x06000396 RID: 918 RVA: 0x0000B2E4 File Offset: 0x000094E4
		public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
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
