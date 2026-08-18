using System;
using System.CodeDom;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000297 RID: 663
	public class AxParameterData
	{
		// Token: 0x06001946 RID: 6470 RVA: 0x0008DB1C File Offset: 0x0008BD1C
		public AxParameterData(string inname, string typeName)
		{
			this.Name = inname;
			this.typeName = typeName;
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0008DB32 File Offset: 0x0008BD32
		public AxParameterData(string inname, Type type)
		{
			this.Name = inname;
			this.type = type;
			this.typeName = AxWrapperGen.MapTypeName(type);
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0008DB54 File Offset: 0x0008BD54
		public AxParameterData(ParameterInfo info) : this(info, false)
		{
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0008DB60 File Offset: 0x0008BD60
		public AxParameterData(ParameterInfo info, bool ignoreByRefs)
		{
			this.paramInfo = info;
			this.Name = info.Name;
			this.type = info.ParameterType;
			this.typeName = AxWrapperGen.MapTypeName(info.ParameterType);
			this.isByRef = (info.ParameterType.IsByRef && !ignoreByRefs);
			this.isIn = (info.IsIn && !ignoreByRefs);
			this.isOut = (info.IsOut && !this.isIn && !ignoreByRefs);
			this.isOptional = info.IsOptional;
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x0008DBFB File Offset: 0x0008BDFB
		public FieldDirection Direction
		{
			get
			{
				if (this.IsOut)
				{
					return FieldDirection.Out;
				}
				if (this.IsByRef)
				{
					return FieldDirection.Ref;
				}
				return FieldDirection.In;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x0008DC12 File Offset: 0x0008BE12
		public bool IsByRef
		{
			get
			{
				return this.isByRef;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x0008DC1A File Offset: 0x0008BE1A
		public bool IsIn
		{
			get
			{
				return this.isIn;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x0008DC22 File Offset: 0x0008BE22
		public bool IsOut
		{
			get
			{
				return this.isOut;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x0008DC2A File Offset: 0x0008BE2A
		public bool IsOptional
		{
			get
			{
				return this.isOptional;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x0008DC32 File Offset: 0x0008BE32
		// (set) Token: 0x06001950 RID: 6480 RVA: 0x0008DC3C File Offset: 0x0008BE3C
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					this.name = null;
					return;
				}
				if (value != null && value.Length > 0 && char.IsUpper(value[0]))
				{
					char[] array = value.ToCharArray();
					if (array.Length != 0)
					{
						array[0] = char.ToLower(array[0], CultureInfo.InvariantCulture);
					}
					this.name = new string(array);
					return;
				}
				this.name = value;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x0008DC9D File Offset: 0x0008BE9D
		public Type ParameterType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0008DCA8 File Offset: 0x0008BEA8
		internal static Type GetByRefBaseType(Type t)
		{
			if (t.IsByRef && t.FullName.EndsWith("&"))
			{
				Type type = t.Assembly.GetType(t.FullName.Substring(0, t.FullName.Length - 1), false);
				if (type != null)
				{
					t = type;
				}
			}
			return t;
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x0008DD02 File Offset: 0x0008BF02
		internal ParameterInfo ParameterInfo
		{
			get
			{
				return this.paramInfo;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x0008DD0A File Offset: 0x0008BF0A
		internal Type ParameterBaseType
		{
			get
			{
				return AxParameterData.GetByRefBaseType(this.ParameterType);
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x0008DD18 File Offset: 0x0008BF18
		public string TypeName
		{
			get
			{
				if (this.typeName == null)
				{
					this.typeName = this.ParameterBaseType.FullName;
				}
				else if (this.typeName.EndsWith("&"))
				{
					this.typeName = this.typeName.TrimEnd(new char[]
					{
						'&'
					});
				}
				return this.typeName;
			}
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0008DD74 File Offset: 0x0008BF74
		public static AxParameterData[] Convert(ParameterInfo[] infos)
		{
			return AxParameterData.Convert(infos, false);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0008DD80 File Offset: 0x0008BF80
		public static AxParameterData[] Convert(ParameterInfo[] infos, bool ignoreByRefs)
		{
			if (infos == null)
			{
				return new AxParameterData[0];
			}
			int num = 0;
			AxParameterData[] array = new AxParameterData[infos.Length];
			for (int i = 0; i < infos.Length; i++)
			{
				array[i] = new AxParameterData(infos[i], ignoreByRefs);
				if (array[i].Name == null || array[i].Name == "")
				{
					array[i].Name = "param" + num++.ToString();
				}
			}
			return array;
		}

		// Token: 0x04001573 RID: 5491
		private string name;

		// Token: 0x04001574 RID: 5492
		private string typeName;

		// Token: 0x04001575 RID: 5493
		private Type type;

		// Token: 0x04001576 RID: 5494
		private bool isByRef;

		// Token: 0x04001577 RID: 5495
		private bool isOut;

		// Token: 0x04001578 RID: 5496
		private bool isIn;

		// Token: 0x04001579 RID: 5497
		private bool isOptional;

		// Token: 0x0400157A RID: 5498
		private ParameterInfo paramInfo;
	}
}
