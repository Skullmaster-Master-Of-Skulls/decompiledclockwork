using System;
using System.CodeDom;
using System.Globalization;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001F5 RID: 501
	internal class ResourcePropertyMemberCodeDomSerializer : MemberCodeDomSerializer
	{
		// Token: 0x060012FF RID: 4863 RVA: 0x0006EF97 File Offset: 0x0006D197
		internal ResourcePropertyMemberCodeDomSerializer(MemberCodeDomSerializer serializer, CodeDomLocalizationProvider.LanguageExtenders extender, CodeDomLocalizationModel model)
		{
			this._serializer = serializer;
			this._extender = extender;
			this._model = model;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0006EFB4 File Offset: 0x0006D1B4
		public override void Serialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor, CodeStatementCollection statements)
		{
			manager.Context.Push(this._model);
			try
			{
				this._serializer.Serialize(manager, value, descriptor, statements);
			}
			finally
			{
				manager.Context.Pop();
			}
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0006F008 File Offset: 0x0006D208
		private CultureInfo GetLocalizationLanguage(IDesignerSerializationManager manager)
		{
			if (this.localizationLanguage == null)
			{
				RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
				if (rootContext != null)
				{
					object value = rootContext.Value;
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(value)["LoadLanguage"];
					if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(CultureInfo))
					{
						this.localizationLanguage = (CultureInfo)propertyDescriptor.GetValue(value);
					}
				}
			}
			return this.localizationLanguage;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0006F088 File Offset: 0x0006D288
		private void OnSerializationComplete(object sender, EventArgs e)
		{
			this.localizationLanguage = null;
			IDesignerSerializationManager designerSerializationManager = sender as IDesignerSerializationManager;
			if (designerSerializationManager != null)
			{
				designerSerializationManager.SerializationComplete -= this.OnSerializationComplete;
			}
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0006F0B8 File Offset: 0x0006D2B8
		public override bool ShouldSerialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor)
		{
			bool flag = this._serializer.ShouldSerialize(manager, value, descriptor);
			if (!flag && !descriptor.Attributes.Contains(DesignOnlyAttribute.Yes))
			{
				CodeDomLocalizationModel model = this._model;
				if (model != CodeDomLocalizationModel.PropertyAssignment)
				{
					if (model == CodeDomLocalizationModel.PropertyReflection && !flag)
					{
						if (this.localizationLanguage == null)
						{
							manager.SerializationComplete += this.OnSerializationComplete;
						}
						if (this.GetLocalizationLanguage(manager) != CultureInfo.InvariantCulture)
						{
							flag = true;
						}
					}
				}
				else
				{
					InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)manager.Context[typeof(InheritanceAttribute)];
					if (inheritanceAttribute == null)
					{
						inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(value)[typeof(InheritanceAttribute)];
						if (inheritanceAttribute == null)
						{
							inheritanceAttribute = InheritanceAttribute.NotInherited;
						}
					}
					if (inheritanceAttribute.InheritanceLevel != InheritanceLevel.InheritedReadOnly)
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x04000A53 RID: 2643
		private CodeDomLocalizationModel _model;

		// Token: 0x04000A54 RID: 2644
		private MemberCodeDomSerializer _serializer;

		// Token: 0x04000A55 RID: 2645
		private CodeDomLocalizationProvider.LanguageExtenders _extender;

		// Token: 0x04000A56 RID: 2646
		private CultureInfo localizationLanguage;
	}
}
