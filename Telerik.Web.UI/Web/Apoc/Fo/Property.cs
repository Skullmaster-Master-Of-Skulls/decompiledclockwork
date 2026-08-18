using System;
using System.Collections;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Fo.Expr;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x0200138E RID: 5006
	internal class Property
	{
		// Token: 0x170042D8 RID: 17112
		// (get) Token: 0x0600D09C RID: 53404 RVA: 0x002E368A File Offset: 0x002E188A
		// (set) Token: 0x0600D09D RID: 53405 RVA: 0x002E3692 File Offset: 0x002E1892
		public string SpecifiedValue
		{
			get
			{
				return this.specVal;
			}
			set
			{
				this.specVal = value;
			}
		}

		// Token: 0x0600D09E RID: 53406 RVA: 0x002E369B File Offset: 0x002E189B
		public virtual Length GetLength()
		{
			return null;
		}

		// Token: 0x0600D09F RID: 53407 RVA: 0x002E369E File Offset: 0x002E189E
		public virtual ColorType GetColorType()
		{
			return null;
		}

		// Token: 0x0600D0A0 RID: 53408 RVA: 0x002E36A1 File Offset: 0x002E18A1
		public virtual CondLength GetCondLength()
		{
			return null;
		}

		// Token: 0x0600D0A1 RID: 53409 RVA: 0x002E36A4 File Offset: 0x002E18A4
		public virtual LengthRange GetLengthRange()
		{
			return null;
		}

		// Token: 0x0600D0A2 RID: 53410 RVA: 0x002E36A7 File Offset: 0x002E18A7
		public virtual LengthPair GetLengthPair()
		{
			return null;
		}

		// Token: 0x0600D0A3 RID: 53411 RVA: 0x002E36AA File Offset: 0x002E18AA
		public virtual Space GetSpace()
		{
			return null;
		}

		// Token: 0x0600D0A4 RID: 53412 RVA: 0x002E36AD File Offset: 0x002E18AD
		public virtual Keep GetKeep()
		{
			return null;
		}

		// Token: 0x0600D0A5 RID: 53413 RVA: 0x002E36B0 File Offset: 0x002E18B0
		public virtual int GetEnum()
		{
			return 0;
		}

		// Token: 0x0600D0A6 RID: 53414 RVA: 0x002E36B3 File Offset: 0x002E18B3
		public virtual char GetCharacter()
		{
			return '\0';
		}

		// Token: 0x0600D0A7 RID: 53415 RVA: 0x002E36B6 File Offset: 0x002E18B6
		public virtual ArrayList GetList()
		{
			return null;
		}

		// Token: 0x0600D0A8 RID: 53416 RVA: 0x002E36B9 File Offset: 0x002E18B9
		public virtual Number GetNumber()
		{
			return null;
		}

		// Token: 0x0600D0A9 RID: 53417 RVA: 0x002E36BC File Offset: 0x002E18BC
		public virtual Numeric GetNumeric()
		{
			return null;
		}

		// Token: 0x0600D0AA RID: 53418 RVA: 0x002E36BF File Offset: 0x002E18BF
		public virtual string GetNCname()
		{
			return null;
		}

		// Token: 0x0600D0AB RID: 53419 RVA: 0x002E36C2 File Offset: 0x002E18C2
		public virtual object GetObject()
		{
			return null;
		}

		// Token: 0x0600D0AC RID: 53420 RVA: 0x002E36C8 File Offset: 0x002E18C8
		public virtual string GetString()
		{
			object @object = this.GetObject();
			if (@object != null)
			{
				return @object.ToString();
			}
			return null;
		}

		// Token: 0x040037FE RID: 14334
		private string specVal;
	}
}
