using System;
using System.CodeDom;
using System.Globalization;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x0200058E RID: 1422
	internal class ResourcePropertyMemberCodeDomSerializer : MemberCodeDomSerializer
	{
		// Token: 0x06003269 RID: 12905 RVA: 0x0011D49E File Offset: 0x0011C49E
		internal ResourcePropertyMemberCodeDomSerializer(MemberCodeDomSerializer serializer, CodeDomLocalizationProvider.LanguageExtenders extender, CodeDomLocalizationModel model)
		{
			this._serializer = serializer;
			this._extender = extender;
			this._model = model;
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x0011D4BC File Offset: 0x0011C4BC
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

		// Token: 0x0600326B RID: 12907 RVA: 0x0011D510 File Offset: 0x0011C510
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

		// Token: 0x0600326C RID: 12908 RVA: 0x0011D588 File Offset: 0x0011C588
		private void OnSerializationComplete(object sender, EventArgs e)
		{
			this.localizationLanguage = null;
			IDesignerSerializationManager designerSerializationManager = sender as IDesignerSerializationManager;
			if (designerSerializationManager != null)
			{
				designerSerializationManager.SerializationComplete -= this.OnSerializationComplete;
			}
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x0011D5B8 File Offset: 0x0011C5B8
		public override bool ShouldSerialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor)
		{
			bool flag = this._serializer.ShouldSerialize(manager, value, descriptor);
			if (!flag && !descriptor.Attributes.Contains(DesignOnlyAttribute.Yes))
			{
				switch (this._model)
				{
				case CodeDomLocalizationModel.PropertyAssignment:
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
					break;
				}
				case CodeDomLocalizationModel.PropertyReflection:
					if (!flag)
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
					break;
				}
			}
			return flag;
		}

		// Token: 0x0400217A RID: 8570
		private CodeDomLocalizationModel _model;

		// Token: 0x0400217B RID: 8571
		private MemberCodeDomSerializer _serializer;

		// Token: 0x0400217C RID: 8572
		private CodeDomLocalizationProvider.LanguageExtenders _extender;

		// Token: 0x0400217D RID: 8573
		private CultureInfo localizationLanguage;
	}
}
