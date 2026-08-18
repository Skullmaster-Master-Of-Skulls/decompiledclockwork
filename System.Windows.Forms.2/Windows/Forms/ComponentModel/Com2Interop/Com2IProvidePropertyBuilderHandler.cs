using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x0200049F RID: 1183
	[SuppressUnmanagedCodeSecurity]
	internal class Com2IProvidePropertyBuilderHandler : Com2ExtendedBrowsingHandler
	{
		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06004EB6 RID: 20150 RVA: 0x00144050 File Offset: 0x00142250
		public override Type Interface
		{
			get
			{
				return typeof(NativeMethods.IProvidePropertyBuilder);
			}
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x0014405C File Offset: 0x0014225C
		private bool GetBuilderGuidString(NativeMethods.IProvidePropertyBuilder target, int dispid, ref string strGuidBldr, int[] bldrType)
		{
			bool flag = false;
			string[] array = new string[1];
			if (NativeMethods.Failed(target.MapPropertyToBuilder(dispid, bldrType, array, ref flag)))
			{
				flag = false;
			}
			if (flag && (bldrType[0] & 2) == 0)
			{
				flag = false;
			}
			if (!flag)
			{
				return false;
			}
			if (array[0] == null)
			{
				strGuidBldr = Guid.Empty.ToString();
			}
			else
			{
				strGuidBldr = array[0];
			}
			return true;
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x001440BC File Offset: 0x001422BC
		public override void SetupPropertyHandlers(Com2PropertyDescriptor[] propDesc)
		{
			if (propDesc == null)
			{
				return;
			}
			for (int i = 0; i < propDesc.Length; i++)
			{
				propDesc[i].QueryGetBaseAttributes += this.OnGetBaseAttributes;
				propDesc[i].QueryGetTypeConverterAndTypeEditor += this.OnGetTypeConverterAndTypeEditor;
			}
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x00144104 File Offset: 0x00142304
		private void OnGetBaseAttributes(Com2PropertyDescriptor sender, GetAttributesEvent attrEvent)
		{
			NativeMethods.IProvidePropertyBuilder providePropertyBuilder = sender.TargetObject as NativeMethods.IProvidePropertyBuilder;
			if (providePropertyBuilder != null)
			{
				string text = null;
				bool builderGuidString = this.GetBuilderGuidString(providePropertyBuilder, sender.DISPID, ref text, new int[1]);
				if (sender.CanShow && builderGuidString && typeof(UnsafeNativeMethods.IDispatch).IsAssignableFrom(sender.PropertyType))
				{
					attrEvent.Add(BrowsableAttribute.Yes);
				}
			}
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x00144164 File Offset: 0x00142364
		private void OnGetTypeConverterAndTypeEditor(Com2PropertyDescriptor sender, GetTypeConverterAndTypeEditorEvent gveevent)
		{
			object targetObject = sender.TargetObject;
			if (targetObject is NativeMethods.IProvidePropertyBuilder)
			{
				NativeMethods.IProvidePropertyBuilder target = (NativeMethods.IProvidePropertyBuilder)targetObject;
				int[] array = new int[1];
				string guidString = null;
				if (this.GetBuilderGuidString(target, sender.DISPID, ref guidString, array))
				{
					gveevent.TypeEditor = new Com2PropertyBuilderUITypeEditor(sender, guidString, array[0], (UITypeEditor)gveevent.TypeEditor);
				}
			}
		}
	}
}
