using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000F3 RID: 243
	public class RDN
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0001CC88 File Offset: 0x0001BC88
		protected internal virtual string RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0001CCA0 File Offset: 0x0001BCA0
		public virtual string Type
		{
			get
			{
				return (string)this.types[0];
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0001CCC4 File Offset: 0x0001BCC4
		public virtual string[] Types
		{
			get
			{
				string[] array = new string[this.types.Count];
				for (int i = 0; i < this.types.Count; i++)
				{
					array[i] = (string)this.types[i];
				}
				return array;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0001CD14 File Offset: 0x0001BD14
		public virtual string Value
		{
			get
			{
				return (string)this.values[0];
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001CD38 File Offset: 0x0001BD38
		public virtual string[] Values
		{
			get
			{
				string[] array = new string[this.values.Count];
				for (int i = 0; i < this.values.Count; i++)
				{
					array[i] = (string)this.values[i];
				}
				return array;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001CD88 File Offset: 0x0001BD88
		public virtual bool Multivalued
		{
			get
			{
				return this.values.Count > 1;
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001CDAC File Offset: 0x0001BDAC
		public RDN(string rdn)
		{
			this.rawValue = rdn;
			DN dn = new DN(rdn);
			ArrayList rdns = dn.RDNs;
			if (rdns.Count != 1)
			{
				throw new ArgumentException("Invalid RDN: see API documentation");
			}
			RDN rdn2 = (RDN)rdns[0];
			this.types = rdn2.types;
			this.values = rdn2.values;
			this.rawValue = rdn2.rawValue;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001CE1C File Offset: 0x0001BE1C
		public RDN()
		{
			this.types = new ArrayList();
			this.values = new ArrayList();
			this.rawValue = "";
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001CE54 File Offset: 0x0001BE54
		[CLSCompliant(false)]
		public virtual bool equals(RDN rdn)
		{
			bool result;
			if (this.values.Count != rdn.values.Count)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < this.values.Count; i++)
				{
					int num = 0;
					while (num < this.values.Count && (!((string)this.values[i]).ToUpper().Equals(((string)rdn.values[num]).ToUpper()) || !this.equalAttrType((string)this.types[i], (string)rdn.types[num])))
					{
						num++;
					}
					if (num >= rdn.values.Count)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001CF28 File Offset: 0x0001BF28
		private bool equalAttrType(string attr1, string attr2)
		{
			if (char.IsDigit(attr1[0]) ^ char.IsDigit(attr2[0]))
			{
				throw new ArgumentException("OID numbers are not currently compared to attribute names");
			}
			return attr1.ToUpper().Equals(attr2.ToUpper());
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001CF70 File Offset: 0x0001BF70
		public virtual void add(string attrType, string attrValue, string rawValue)
		{
			this.types.Add(attrType);
			this.values.Add(attrValue);
			this.rawValue += rawValue;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001CFAC File Offset: 0x0001BFAC
		public override string ToString()
		{
			return this.toString(false);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001CFC4 File Offset: 0x0001BFC4
		[CLSCompliant(false)]
		public virtual string toString(bool noTypes)
		{
			int count = this.types.Count;
			string text = "";
			string result;
			if (count < 1)
			{
				result = null;
			}
			else
			{
				if (!noTypes)
				{
					text = this.types[0] + "=";
				}
				text += this.values[0];
				for (int i = 1; i < count; i++)
				{
					text += "+";
					if (!noTypes)
					{
						text = text + this.types[i] + "=";
					}
					text += this.values[i];
				}
				result = text;
			}
			return result;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001D064 File Offset: 0x0001C064
		public virtual string[] explodeRDN(bool noTypes)
		{
			int count = this.types.Count;
			string[] result;
			if (count < 1)
			{
				result = null;
			}
			else
			{
				string[] array = new string[this.types.Count];
				if (!noTypes)
				{
					array[0] = this.types[0] + "=";
				}
				string[] array2;
				(array2 = array)[0] = array2[0] + this.values[0];
				for (int i = 1; i < count; i++)
				{
					IntPtr intPtr;
					if (!noTypes)
					{
						(array2 = array)[(int)(intPtr = (IntPtr)i)] = array2[(int)intPtr] + this.types[i] + "=";
					}
					(array2 = array)[(int)(intPtr = (IntPtr)i)] = array2[(int)intPtr] + this.values[i];
				}
				result = array;
			}
			return result;
		}

		// Token: 0x04000486 RID: 1158
		private ArrayList types;

		// Token: 0x04000487 RID: 1159
		private ArrayList values;

		// Token: 0x04000488 RID: 1160
		private string rawValue;
	}
}
