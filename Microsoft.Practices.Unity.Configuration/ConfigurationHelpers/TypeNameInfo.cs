using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x02000011 RID: 17
	internal class TypeNameInfo
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002F3C File Offset: 0x0000113C
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002F44 File Offset: 0x00001144
		public string Name { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002F4D File Offset: 0x0000114D
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002F55 File Offset: 0x00001155
		public string Namespace { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002F5E File Offset: 0x0000115E
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002F66 File Offset: 0x00001166
		public string AssemblyName { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002F6F File Offset: 0x0000116F
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002F77 File Offset: 0x00001177
		public bool IsGenericType { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002F80 File Offset: 0x00001180
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002F88 File Offset: 0x00001188
		public bool IsOpenGeneric { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002F91 File Offset: 0x00001191
		public int NumGenericParameters
		{
			get
			{
				return this.GenericParameters.Count;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002F9E File Offset: 0x0000119E
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002FA6 File Offset: 0x000011A6
		public List<TypeNameInfo> GenericParameters { get; private set; }

		// Token: 0x06000070 RID: 112 RVA: 0x00002FAF File Offset: 0x000011AF
		public TypeNameInfo()
		{
			this.GenericParameters = new List<TypeNameInfo>();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002FC4 File Offset: 0x000011C4
		public string FullName
		{
			get
			{
				string text = this.Name;
				if (this.IsGenericType)
				{
					text = text + '`' + this.NumGenericParameters.ToString(CultureInfo.InvariantCulture);
				}
				if (!string.IsNullOrEmpty(this.Namespace))
				{
					text = this.Namespace + '.' + text;
				}
				if (!string.IsNullOrEmpty(this.AssemblyName))
				{
					text = text + ", " + this.AssemblyName;
				}
				return text;
			}
		}
	}
}
