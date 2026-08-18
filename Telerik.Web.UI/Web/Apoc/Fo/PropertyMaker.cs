using System;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Fo.Expr;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x02001390 RID: 5008
	internal class PropertyMaker
	{
		// Token: 0x170042D9 RID: 17113
		// (get) Token: 0x0600D0AF RID: 53423 RVA: 0x002E3711 File Offset: 0x002E1911
		protected string PropName
		{
			get
			{
				return this.propName;
			}
		}

		// Token: 0x0600D0B0 RID: 53424 RVA: 0x002E3719 File Offset: 0x002E1919
		protected PropertyMaker()
		{
			this.propName = "UNKNOWN";
		}

		// Token: 0x0600D0B1 RID: 53425 RVA: 0x002E372C File Offset: 0x002E192C
		protected PropertyMaker(string propName)
		{
			this.propName = propName;
		}

		// Token: 0x0600D0B2 RID: 53426 RVA: 0x002E373B File Offset: 0x002E193B
		public virtual bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D0B3 RID: 53427 RVA: 0x002E373E File Offset: 0x002E193E
		public virtual bool InheritsSpecified()
		{
			return false;
		}

		// Token: 0x0600D0B4 RID: 53428 RVA: 0x002E3741 File Offset: 0x002E1941
		public virtual IPercentBase GetPercentBase(FObj fo, PropertyList pl)
		{
			return null;
		}

		// Token: 0x0600D0B5 RID: 53429 RVA: 0x002E3744 File Offset: 0x002E1944
		protected virtual PropertyMaker GetSubpropMaker(string subprop)
		{
			return null;
		}

		// Token: 0x0600D0B6 RID: 53430 RVA: 0x002E3747 File Offset: 0x002E1947
		public virtual Property GetSubpropValue(Property p, string subprop)
		{
			return null;
		}

		// Token: 0x0600D0B7 RID: 53431 RVA: 0x002E374C File Offset: 0x002E194C
		public Property Make(Property baseProp, string partName, PropertyList propertyList, string value, FObj fo)
		{
			if (baseProp == null)
			{
				baseProp = this.MakeCompound(propertyList, fo);
			}
			PropertyMaker subpropMaker = this.GetSubpropMaker(partName);
			if (subpropMaker != null)
			{
				Property property = subpropMaker.Make(propertyList, value, fo);
				if (property != null)
				{
					return this.SetSubprop(baseProp, partName, property);
				}
			}
			return baseProp;
		}

		// Token: 0x0600D0B8 RID: 53432 RVA: 0x002E378C File Offset: 0x002E198C
		protected virtual Property SetSubprop(Property baseProp, string partName, Property subProp)
		{
			return baseProp;
		}

		// Token: 0x0600D0B9 RID: 53433 RVA: 0x002E3790 File Offset: 0x002E1990
		public virtual Property Make(PropertyList propertyList, string value, FObj fo)
		{
			Property result;
			try
			{
				string text = value;
				Property property = this.CheckEnumValues(value);
				if (property == null)
				{
					text = this.CheckValueKeywords(value);
					Property p = PropertyParser.parse(text, new PropertyInfo(this, propertyList, fo));
					property = this.ConvertProperty(p, propertyList, fo);
				}
				else if (this.IsCompoundMaker())
				{
					property = this.ConvertProperty(property, propertyList, fo);
				}
				if (property != null && this.InheritsSpecified())
				{
					property.SpecifiedValue = text;
				}
				result = property;
			}
			catch (PropertyException ex)
			{
				throw new ApocException(string.Concat(new string[]
				{
					"Error in ",
					this.propName,
					" property value '",
					value,
					"': ",
					ex.Message
				}));
			}
			return result;
		}

		// Token: 0x0600D0BA RID: 53434 RVA: 0x002E3854 File Offset: 0x002E1A54
		public Property ConvertShorthandProperty(PropertyList propertyList, Property prop, FObj fo)
		{
			Property property = null;
			try
			{
				property = this.ConvertProperty(prop, propertyList, fo);
				if (property == null)
				{
					string ncname = prop.GetNCname();
					if (ncname != null)
					{
						property = this.CheckEnumValues(ncname);
						if (property == null)
						{
							string text = this.CheckValueKeywords(ncname);
							if (!text.Equals(ncname))
							{
								Property p = PropertyParser.parse(text, new PropertyInfo(this, propertyList, fo));
								property = this.ConvertProperty(p, propertyList, fo);
							}
						}
					}
				}
			}
			catch (ApocException)
			{
			}
			catch (PropertyException)
			{
			}
			return property;
		}

		// Token: 0x0600D0BB RID: 53435 RVA: 0x002E38D8 File Offset: 0x002E1AD8
		protected virtual bool IsCompoundMaker()
		{
			return false;
		}

		// Token: 0x0600D0BC RID: 53436 RVA: 0x002E38DB File Offset: 0x002E1ADB
		public virtual Property CheckEnumValues(string value)
		{
			return null;
		}

		// Token: 0x0600D0BD RID: 53437 RVA: 0x002E38DE File Offset: 0x002E1ADE
		protected virtual string CheckValueKeywords(string value)
		{
			return value;
		}

		// Token: 0x0600D0BE RID: 53438 RVA: 0x002E38E1 File Offset: 0x002E1AE1
		public virtual Property ConvertProperty(Property p, PropertyList propertyList, FObj fo)
		{
			return null;
		}

		// Token: 0x0600D0BF RID: 53439 RVA: 0x002E38E4 File Offset: 0x002E1AE4
		protected virtual Property ConvertPropertyDatatype(Property p, PropertyList propertyList, FObj fo)
		{
			return null;
		}

		// Token: 0x0600D0C0 RID: 53440 RVA: 0x002E38E7 File Offset: 0x002E1AE7
		public virtual Property Make(PropertyList propertyList)
		{
			return null;
		}

		// Token: 0x0600D0C1 RID: 53441 RVA: 0x002E38EA File Offset: 0x002E1AEA
		protected virtual Property MakeCompound(PropertyList propertyList, FObj parentFO)
		{
			return null;
		}

		// Token: 0x0600D0C2 RID: 53442 RVA: 0x002E38F0 File Offset: 0x002E1AF0
		public virtual Property Compute(PropertyList propertyList)
		{
			if (this.InheritsSpecified())
			{
				Property nearestSpecifiedProperty = propertyList.GetNearestSpecifiedProperty(this.propName);
				if (nearestSpecifiedProperty != null)
				{
					string specifiedValue = nearestSpecifiedProperty.SpecifiedValue;
					if (specifiedValue != null)
					{
						try
						{
							return this.Make(propertyList, specifiedValue, propertyList.getParentFObj());
						}
						catch (ApocException)
						{
							return null;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600D0C3 RID: 53443 RVA: 0x002E3948 File Offset: 0x002E1B48
		public virtual bool IsCorrespondingForced(PropertyList propertyList)
		{
			return false;
		}

		// Token: 0x0600D0C4 RID: 53444 RVA: 0x002E394B File Offset: 0x002E1B4B
		public virtual Property GetShorthand(PropertyList propertyList)
		{
			return null;
		}

		// Token: 0x0600D0C5 RID: 53445 RVA: 0x002E394E File Offset: 0x002E1B4E
		public static PropertyMaker Maker(string propName)
		{
			throw new Exception("This method should not be called!");
		}

		// Token: 0x040037FF RID: 14335
		private const string UNKNOWN = "UNKNOWN";

		// Token: 0x04003800 RID: 14336
		private string propName;
	}
}
