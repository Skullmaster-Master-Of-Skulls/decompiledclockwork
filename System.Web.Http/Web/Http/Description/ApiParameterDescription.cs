using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;

namespace System.Web.Http.Description
{
	// Token: 0x020000BF RID: 191
	public class ApiParameterDescription
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000E49C File Offset: 0x0000C69C
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000E4A4 File Offset: 0x0000C6A4
		public string Name { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000E4AD File Offset: 0x0000C6AD
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000E4B5 File Offset: 0x0000C6B5
		public string Documentation { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000E4BE File Offset: 0x0000C6BE
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0000E4C6 File Offset: 0x0000C6C6
		public ApiParameterSource Source { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000E4CF File Offset: 0x0000C6CF
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000E4D7 File Offset: 0x0000C6D7
		public HttpParameterDescriptor ParameterDescriptor { get; set; }

		// Token: 0x0600046F RID: 1135 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		internal IEnumerable<PropertyInfo> GetBindableProperties()
		{
			return ApiParameterDescription.GetBindableProperties(this.ParameterDescriptor.ParameterType);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000E4FF File Offset: 0x0000C6FF
		internal bool CanConvertPropertiesFromString()
		{
			return this.GetBindableProperties().All((PropertyInfo p) => TypeHelper.CanConvertFromString(p.PropertyType));
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000E547 File Offset: 0x0000C747
		internal static IEnumerable<PropertyInfo> GetBindableProperties(Type type)
		{
			return from p in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
			where p.GetGetMethod() != null && p.GetSetMethod() != null
			select p;
		}
	}
}
