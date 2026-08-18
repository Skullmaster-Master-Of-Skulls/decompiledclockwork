using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000EA RID: 234
	public class AttributeQualifier
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001AD10 File Offset: 0x00019D10
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0001AD28 File Offset: 0x00019D28
		public virtual string[] Values
		{
			get
			{
				string[] array = null;
				if (this.values.Count > 0)
				{
					array = new string[this.values.Count];
					for (int i = 0; i < this.values.Count; i++)
					{
						array[i] = (string)this.values[i];
					}
				}
				return array;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001AD88 File Offset: 0x00019D88
		public AttributeQualifier(string name, string[] value_Renamed)
		{
			if (name == null || value_Renamed == null)
			{
				throw new ArgumentException("A null name or value was passed in for a schema definition qualifier");
			}
			this.name = name;
			this.values = new ArrayList(5);
			for (int i = 0; i < value_Renamed.Length; i++)
			{
				this.values.Add(value_Renamed[i]);
			}
		}

		// Token: 0x04000429 RID: 1065
		internal string name;

		// Token: 0x0400042A RID: 1066
		internal ArrayList values;
	}
}
