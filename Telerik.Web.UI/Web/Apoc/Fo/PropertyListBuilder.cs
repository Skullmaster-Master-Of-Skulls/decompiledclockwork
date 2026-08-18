using System;
using System.Collections;
using System.Collections.Specialized;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020015BE RID: 5566
	internal sealed class PropertyListBuilder
	{
		// Token: 0x0600D929 RID: 55593 RVA: 0x002FA371 File Offset: 0x002F8571
		internal PropertyListBuilder()
		{
		}

		// Token: 0x0600D92A RID: 55594 RVA: 0x002FA384 File Offset: 0x002F8584
		internal void AddList(Hashtable list)
		{
			foreach (object key in list.Keys)
			{
				this.propertyListTable.Add(key, list[key]);
			}
		}

		// Token: 0x0600D92B RID: 55595 RVA: 0x002FA3E4 File Offset: 0x002F85E4
		internal PropertyList MakeList(string ns, string elementName, Attributes attributes, FObj parentFO)
		{
			string text = "http://www.w3.org/TR/1999/XSL/Format";
			if (ns != null)
			{
				text = ns;
			}
			PropertyList propertyList = (parentFO != null) ? parentFO.properties : null;
			PropertyList parentPropertyList = null;
			if (propertyList != null && text.Equals(propertyList.GetNameSpace()))
			{
				parentPropertyList = propertyList;
			}
			PropertyList propertyList2 = new PropertyList(parentPropertyList, text, elementName);
			propertyList2.SetBuilder(this);
			StringCollection stringCollection = new StringCollection();
			string value = attributes.getValue("font-size");
			if (value != null)
			{
				PropertyMaker propertyMaker = this.FindMaker("font-size");
				if (propertyMaker != null)
				{
					try
					{
						propertyList2.Add("font-size", propertyMaker.Make(propertyList2, value, parentFO));
					}
					catch (ApocException)
					{
					}
				}
				stringCollection.Add("font-size");
			}
			int i = 0;
			while (i < attributes.getLength())
			{
				string qname = attributes.getQName(i);
				int num = qname.IndexOf('.');
				string text2 = qname;
				string text3 = null;
				if (num > -1)
				{
					text2 = qname.Substring(0, num);
					text3 = qname.Substring(num + 1);
					goto IL_E6;
				}
				if (!stringCollection.Contains(text2))
				{
					goto IL_E6;
				}
				IL_1A2:
				i++;
				continue;
				IL_E6:
				PropertyMaker propertyMaker2 = this.FindMaker(text2);
				if (propertyMaker2 != null)
				{
					try
					{
						Property property2;
						if (text3 != null)
						{
							Property property = propertyList2.GetExplicitBaseProperty(text2);
							if (property == null)
							{
								string value2 = attributes.getValue(text2);
								if (value2 != null)
								{
									property = propertyMaker2.Make(propertyList2, value2, parentFO);
									stringCollection.Add(text2);
								}
							}
							property2 = propertyMaker2.Make(property, text3, propertyList2, attributes.getValue(i), parentFO);
						}
						else
						{
							property2 = propertyMaker2.Make(propertyList2, attributes.getValue(i), parentFO);
						}
						if (property2 != null)
						{
							propertyList2[text2] = property2;
						}
						goto IL_1A2;
					}
					catch (ApocException ex)
					{
						string message = ex.Message;
						goto IL_1A2;
					}
				}
				if (!qname.StartsWith("xmlns"))
				{
					ApocDriver.ActiveDriver.FireApocWarning("property " + qname + " ignored");
					goto IL_1A2;
				}
				goto IL_1A2;
			}
			return propertyList2;
		}

		// Token: 0x0600D92C RID: 55596 RVA: 0x002FA5C4 File Offset: 0x002F87C4
		internal Property GetSubpropValue(string propertyName, Property p, string subpropName)
		{
			PropertyMaker propertyMaker = this.FindMaker(propertyName);
			if (propertyMaker != null)
			{
				return propertyMaker.GetSubpropValue(p, subpropName);
			}
			return null;
		}

		// Token: 0x0600D92D RID: 55597 RVA: 0x002FA5E8 File Offset: 0x002F87E8
		internal Property GetShorthand(PropertyList propertyList, string propertyName)
		{
			PropertyMaker propertyMaker = this.FindMaker(propertyName);
			if (propertyMaker != null)
			{
				return propertyMaker.GetShorthand(propertyList);
			}
			ApocDriver.ActiveDriver.FireApocError("No maker for " + propertyName);
			return null;
		}

		// Token: 0x0600D92E RID: 55598 RVA: 0x002FA620 File Offset: 0x002F8820
		internal Property MakeProperty(PropertyList propertyList, string propertyName)
		{
			Property result = null;
			PropertyMaker propertyMaker = this.FindMaker(propertyName);
			if (propertyMaker != null)
			{
				result = propertyMaker.Make(propertyList);
			}
			else
			{
				ApocDriver.ActiveDriver.FireApocWarning("property " + propertyName + " ignored");
			}
			return result;
		}

		// Token: 0x0600D92F RID: 55599 RVA: 0x002FA65F File Offset: 0x002F885F
		internal PropertyMaker FindMaker(string propertyName)
		{
			return (PropertyMaker)this.propertyListTable[propertyName];
		}

		// Token: 0x04003BFC RID: 15356
		private const string FONTSIZEATTR = "font-size";

		// Token: 0x04003BFD RID: 15357
		private Hashtable propertyListTable = new Hashtable();
	}
}
