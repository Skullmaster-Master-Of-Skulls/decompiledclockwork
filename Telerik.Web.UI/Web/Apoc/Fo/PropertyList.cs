using System;
using System.Collections;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020015BD RID: 5565
	internal class PropertyList : Hashtable
	{
		// Token: 0x0600D912 RID: 55570 RVA: 0x002F9D90 File Offset: 0x002F7F90
		static PropertyList()
		{
			PropertyList.wmtables.Add(41, new byte[]
			{
				0,
				1,
				2,
				3,
				4,
				5
			});
			PropertyList.wmtables.Add(65, new byte[]
			{
				1,
				0,
				2,
				3,
				4,
				5
			});
			PropertyList.wmtables.Add(76, new byte[]
			{
				3,
				2,
				0,
				1,
				5,
				4
			});
		}

		// Token: 0x0600D913 RID: 55571 RVA: 0x002F9E88 File Offset: 0x002F8088
		public PropertyList(PropertyList parentPropertyList, string space, string el)
		{
			this.parentPropertyList = parentPropertyList;
			this.nmspace = space;
			this.element = el;
		}

		// Token: 0x170042F6 RID: 17142
		// (get) Token: 0x0600D914 RID: 55572 RVA: 0x002F9EBB File Offset: 0x002F80BB
		// (set) Token: 0x0600D915 RID: 55573 RVA: 0x002F9EC3 File Offset: 0x002F80C3
		public FObj FObj
		{
			get
			{
				return this.fobj;
			}
			set
			{
				this.fobj = value;
			}
		}

		// Token: 0x0600D916 RID: 55574 RVA: 0x002F9ECC File Offset: 0x002F80CC
		public FObj getParentFObj()
		{
			if (this.parentPropertyList != null)
			{
				return this.parentPropertyList.FObj;
			}
			return null;
		}

		// Token: 0x0600D917 RID: 55575 RVA: 0x002F9EE4 File Offset: 0x002F80E4
		public Property GetExplicitOrShorthandProperty(string propertyName)
		{
			int num = propertyName.IndexOf('.');
			string propertyName2;
			if (num > -1)
			{
				propertyName2 = propertyName.Substring(0, num);
			}
			else
			{
				propertyName2 = propertyName;
			}
			Property property = this.GetExplicitBaseProperty(propertyName2);
			if (property == null)
			{
				property = this.builder.GetShorthand(this, propertyName2);
			}
			if (property != null && num > -1)
			{
				return this.builder.GetSubpropValue(propertyName2, property, propertyName.Substring(num + 1));
			}
			return property;
		}

		// Token: 0x0600D918 RID: 55576 RVA: 0x002F9F44 File Offset: 0x002F8144
		public Property GetExplicitProperty(string propertyName)
		{
			int num = propertyName.IndexOf('.');
			if (num <= -1)
			{
				return (Property)this[propertyName];
			}
			string propertyName2 = propertyName.Substring(0, num);
			Property explicitBaseProperty = this.GetExplicitBaseProperty(propertyName2);
			if (explicitBaseProperty != null)
			{
				return this.builder.GetSubpropValue(propertyName2, explicitBaseProperty, propertyName.Substring(num + 1));
			}
			return null;
		}

		// Token: 0x0600D919 RID: 55577 RVA: 0x002F9F97 File Offset: 0x002F8197
		public Property GetExplicitBaseProperty(string propertyName)
		{
			return (Property)this[propertyName];
		}

		// Token: 0x0600D91A RID: 55578 RVA: 0x002F9FA8 File Offset: 0x002F81A8
		public Property GetInheritedProperty(string propertyName)
		{
			if (this.builder != null)
			{
				if (this.parentPropertyList != null && this.IsInherited(propertyName))
				{
					return this.parentPropertyList.GetProperty(propertyName);
				}
				try
				{
					return this.builder.MakeProperty(this, propertyName);
				}
				catch (ApocException ex)
				{
					ApocDriver.ActiveDriver.FireApocError(string.Concat(new object[]
					{
						"Exception in getInherited(): property=",
						propertyName,
						" : ",
						ex
					}));
				}
			}
			return null;
		}

		// Token: 0x0600D91B RID: 55579 RVA: 0x002FA030 File Offset: 0x002F8230
		private bool IsInherited(string propertyName)
		{
			PropertyMaker propertyMaker = this.builder.FindMaker(propertyName);
			if (propertyMaker != null)
			{
				return propertyMaker.IsInherited();
			}
			ApocDriver.ActiveDriver.FireApocError("Unknown property : " + propertyName);
			return true;
		}

		// Token: 0x0600D91C RID: 55580 RVA: 0x002FA06C File Offset: 0x002F826C
		private Property FindProperty(string propertyName, bool bTryInherit)
		{
			PropertyMaker propertyMaker = this.builder.FindMaker(propertyName);
			Property property;
			if (propertyMaker.IsCorrespondingForced(this))
			{
				property = this.ComputeProperty(this, propertyMaker);
			}
			else
			{
				property = this.GetExplicitBaseProperty(propertyName);
				if (property == null)
				{
					property = this.ComputeProperty(this, propertyMaker);
				}
				if (property == null)
				{
					property = propertyMaker.GetShorthand(this);
				}
				if (property == null && bTryInherit && this.parentPropertyList != null && propertyMaker.IsInherited())
				{
					property = this.parentPropertyList.FindProperty(propertyName, true);
				}
			}
			return property;
		}

		// Token: 0x0600D91D RID: 55581 RVA: 0x002FA0E0 File Offset: 0x002F82E0
		private Property ComputeProperty(PropertyList propertyList, PropertyMaker propertyMaker)
		{
			Property result = null;
			try
			{
				result = propertyMaker.Compute(propertyList);
			}
			catch (ApocException ex)
			{
				ApocDriver.ActiveDriver.FireApocError(ex.Message);
			}
			return result;
		}

		// Token: 0x0600D91E RID: 55582 RVA: 0x002FA120 File Offset: 0x002F8320
		public Property GetSpecifiedProperty(string propertyName)
		{
			return this.GetProperty(propertyName, false, false);
		}

		// Token: 0x0600D91F RID: 55583 RVA: 0x002FA12B File Offset: 0x002F832B
		public Property GetProperty(string propertyName)
		{
			return this.GetProperty(propertyName, true, true);
		}

		// Token: 0x0600D920 RID: 55584 RVA: 0x002FA138 File Offset: 0x002F8338
		private Property GetProperty(string propertyName, bool bTryInherit, bool bTryDefault)
		{
			if (this.builder == null)
			{
				ApocDriver.ActiveDriver.FireApocError("builder not set in PropertyList");
			}
			int num = propertyName.IndexOf('.');
			string text = null;
			if (num > -1)
			{
				text = propertyName.Substring(num + 1);
				propertyName = propertyName.Substring(0, num);
			}
			Property property = this.FindProperty(propertyName, bTryInherit);
			if (property == null && bTryDefault)
			{
				try
				{
					property = this.builder.MakeProperty(this, propertyName);
				}
				catch (ApocException ex)
				{
					ApocDriver.ActiveDriver.FireApocError(ex.ToString());
				}
			}
			if (text != null && property != null)
			{
				return this.builder.GetSubpropValue(propertyName, property, text);
			}
			return property;
		}

		// Token: 0x0600D921 RID: 55585 RVA: 0x002FA1D8 File Offset: 0x002F83D8
		public void SetBuilder(PropertyListBuilder builder)
		{
			this.builder = builder;
		}

		// Token: 0x0600D922 RID: 55586 RVA: 0x002FA1E1 File Offset: 0x002F83E1
		public string GetNameSpace()
		{
			return this.nmspace;
		}

		// Token: 0x0600D923 RID: 55587 RVA: 0x002FA1E9 File Offset: 0x002F83E9
		public string GetElement()
		{
			return this.element;
		}

		// Token: 0x0600D924 RID: 55588 RVA: 0x002FA1F4 File Offset: 0x002F83F4
		public Property GetNearestSpecifiedProperty(string propertyName)
		{
			Property property = null;
			PropertyList propertyList = this;
			while (property == null && propertyList != null)
			{
				property = propertyList.GetExplicitProperty(propertyName);
				propertyList = propertyList.parentPropertyList;
			}
			if (property == null)
			{
				try
				{
					property = this.builder.MakeProperty(this, propertyName);
				}
				catch (ApocException ex)
				{
					ApocDriver.ActiveDriver.FireApocError(string.Concat(new object[]
					{
						"Exception in getNearestSpecified(): property=",
						propertyName,
						" : ",
						ex
					}));
				}
			}
			return property;
		}

		// Token: 0x0600D925 RID: 55589 RVA: 0x002FA274 File Offset: 0x002F8474
		public Property GetFromParentProperty(string propertyName)
		{
			if (this.parentPropertyList != null)
			{
				return this.parentPropertyList.GetProperty(propertyName);
			}
			if (this.builder != null)
			{
				try
				{
					return this.builder.MakeProperty(this, propertyName);
				}
				catch (ApocException ex)
				{
					ApocDriver.ActiveDriver.FireApocError(string.Concat(new object[]
					{
						"Exception in getFromParent(): property=",
						propertyName,
						" : ",
						ex
					}));
				}
			}
			return null;
		}

		// Token: 0x0600D926 RID: 55590 RVA: 0x002FA2F4 File Offset: 0x002F84F4
		public string wmAbsToRel(int absdir)
		{
			if (this.wmtable != null)
			{
				return PropertyList.sRelNames[(int)this.wmtable[absdir]];
			}
			return string.Empty;
		}

		// Token: 0x0600D927 RID: 55591 RVA: 0x002FA314 File Offset: 0x002F8514
		public string wmRelToAbs(int reldir)
		{
			if (this.wmtable != null)
			{
				for (int i = 0; i < this.wmtable.Length; i++)
				{
					if ((int)this.wmtable[i] == reldir)
					{
						return PropertyList.sAbsNames[i];
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x0600D928 RID: 55592 RVA: 0x002FA354 File Offset: 0x002F8554
		public void SetWritingMode(int writingMode)
		{
			this.wmtable = (byte[])PropertyList.wmtables[writingMode];
		}

		// Token: 0x04003BE7 RID: 15335
		public const int LEFT = 0;

		// Token: 0x04003BE8 RID: 15336
		public const int RIGHT = 1;

		// Token: 0x04003BE9 RID: 15337
		public const int TOP = 2;

		// Token: 0x04003BEA RID: 15338
		public const int BOTTOM = 3;

		// Token: 0x04003BEB RID: 15339
		public const int HEIGHT = 4;

		// Token: 0x04003BEC RID: 15340
		public const int WIDTH = 5;

		// Token: 0x04003BED RID: 15341
		public const int START = 0;

		// Token: 0x04003BEE RID: 15342
		public const int END = 1;

		// Token: 0x04003BEF RID: 15343
		public const int BEFORE = 2;

		// Token: 0x04003BF0 RID: 15344
		public const int AFTER = 3;

		// Token: 0x04003BF1 RID: 15345
		public const int BLOCKPROGDIM = 4;

		// Token: 0x04003BF2 RID: 15346
		public const int INLINEPROGDIM = 5;

		// Token: 0x04003BF3 RID: 15347
		private byte[] wmtable;

		// Token: 0x04003BF4 RID: 15348
		private static readonly string[] sAbsNames = new string[]
		{
			"left",
			"right",
			"top",
			"bottom",
			"height",
			"width"
		};

		// Token: 0x04003BF5 RID: 15349
		private static readonly string[] sRelNames = new string[]
		{
			"start",
			"end",
			"before",
			"after",
			"block-progression-dimension",
			"inline-progression-dimension"
		};

		// Token: 0x04003BF6 RID: 15350
		private static readonly Hashtable wmtables = new Hashtable(4);

		// Token: 0x04003BF7 RID: 15351
		private PropertyListBuilder builder;

		// Token: 0x04003BF8 RID: 15352
		private PropertyList parentPropertyList;

		// Token: 0x04003BF9 RID: 15353
		private string nmspace = "";

		// Token: 0x04003BFA RID: 15354
		private string element = "";

		// Token: 0x04003BFB RID: 15355
		private FObj fobj;
	}
}
