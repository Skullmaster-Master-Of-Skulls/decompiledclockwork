using System;
using System.ComponentModel;
using System.Windows.Forms;
using DynamicScreens.DynamicControlWrappers.TypeConverters;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x0200006D RID: 109
	public class DynamicControlWrapper_MultiCheckboxDroplist : DynamicControlWrapper_Base
	{
		// Token: 0x0600056E RID: 1390 RVA: 0x000427AC File Offset: 0x000417AC
		public DynamicControlWrapper_MultiCheckboxDroplist(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000427B8 File Offset: 0x000417B8
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x000427D8 File Offset: 0x000417D8
		[Category("Design")]
		[TypeConverter(typeof(DropListStyleEnumConverter))]
		[Description("Indicates whether the user can only choose from what is within the list, or if they are able to enter items not already in the list.")]
		public DropListBehaviour DropListStyle
		{
			get
			{
				return (DropListBehaviour)this.dynamicControl.Setting3;
			}
			set
			{
				if (this.dynamicControl.ControlId < 0)
				{
					this.dynamicControl.Setting3 = (int)value;
				}
				else
				{
					DynamicControlWrapper_HelperClass dynamicControlWrapper_HelperClass = (DynamicControlWrapper_HelperClass)this.dynamicControl.Tag;
					MessageBox.Show("Any existing data will not be affected by changing this value.  You should use the functions in the 'Functions' menu at the top to convert existing data to the new type.");
					this.dynamicControl.Setting3 = (int)value;
				}
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00042838 File Offset: 0x00041838
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00042855 File Offset: 0x00041855
		[Description("Indicates the number of characters wide this control will be when displayed.")]
		[Category("Display")]
		public int CharacterWidth
		{
			get
			{
				return this.dynamicControl.Setting4;
			}
			set
			{
				this.dynamicControl.Setting4 = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00042868 File Offset: 0x00041868
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x00042880 File Offset: 0x00041880
		[Description("Indicates the list that will be available to the user.")]
		[TypeConverter(typeof(StringIntTypeConverter))]
		[Category("Design")]
		public DynamicControl List
		{
			get
			{
				return this.dynamicControl;
			}
			set
			{
				this.dynamicControl.Setting1 = value.Setting1;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00042898 File Offset: 0x00041898
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x000428C8 File Offset: 0x000418C8
		[Description("Change the name of the currently selected list here.")]
		[Category("Design")]
		public string ListName
		{
			get
			{
				DynamicListGroup selectedDynamicListGroup = this.GetSelectedDynamicListGroup();
				string result;
				if (selectedDynamicListGroup != null)
				{
					result = selectedDynamicListGroup.Description;
				}
				else
				{
					result = "{none}";
				}
				return result;
			}
			set
			{
				DynamicListGroup selectedDynamicListGroup = this.GetSelectedDynamicListGroup();
				if (selectedDynamicListGroup != null)
				{
					selectedDynamicListGroup.Description = value;
				}
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000428F0 File Offset: 0x000418F0
		private DynamicListGroup GetSelectedDynamicListGroup()
		{
			int listGroupId;
			if (this.dynamicControl.IsComboBox)
			{
				listGroupId = this.dynamicControl.Setting1;
			}
			else
			{
				listGroupId = -1;
			}
			DynamicControlWrapper_HelperClass dynamicControlWrapper_HelperClass = (DynamicControlWrapper_HelperClass)this.dynamicControl.Tag;
			return dynamicControlWrapper_HelperClass.ListGroups.FindListGroup(listGroupId);
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00042944 File Offset: 0x00041944
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x0004297D File Offset: 0x0004197D
		[Description("Change the list members / ordering here.")]
		[Category("Design")]
		public DynamicListGroup ListMembers
		{
			get
			{
				DynamicControlWrapper_HelperClass dynamicControlWrapper_HelperClass = (DynamicControlWrapper_HelperClass)this.dynamicControl.Tag;
				return dynamicControlWrapper_HelperClass.ListGroups.FindListGroup(this.dynamicControl.Setting1);
			}
			set
			{
			}
		}
	}
}
