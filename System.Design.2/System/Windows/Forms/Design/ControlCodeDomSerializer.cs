using System;
using System.CodeDom;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002AF RID: 687
	internal class ControlCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x06001ACB RID: 6859 RVA: 0x0009C610 File Offset: 0x0009A810
		public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
		{
			if (manager == null || codeObject == null)
			{
				throw new ArgumentNullException((manager == null) ? "manager" : "codeObject");
			}
			IContainer container = (IContainer)manager.GetService(typeof(IContainer));
			ArrayList arrayList = null;
			if (container != null)
			{
				arrayList = new ArrayList(container.Components.Count);
				foreach (object obj in container.Components)
				{
					IComponent component = (IComponent)obj;
					Control control = component as Control;
					if (control != null)
					{
						control.SuspendLayout();
						arrayList.Add(control);
					}
				}
			}
			object result = null;
			try
			{
				CodeDomSerializer codeDomSerializer = (CodeDomSerializer)manager.GetSerializer(typeof(Component), typeof(CodeDomSerializer));
				if (codeDomSerializer == null)
				{
					return null;
				}
				result = codeDomSerializer.Deserialize(manager, codeObject);
			}
			finally
			{
				if (arrayList != null)
				{
					foreach (object obj2 in arrayList)
					{
						Control control2 = (Control)obj2;
						control2.ResumeLayout(true);
					}
				}
			}
			return result;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0009C764 File Offset: 0x0009A964
		private bool HasAutoSizedChildren(Control parent)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control.AutoSize)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0009C7C8 File Offset: 0x0009A9C8
		private bool HasMixedInheritedChildren(Control parent)
		{
			bool flag = false;
			bool flag2 = false;
			foreach (object obj in parent.Controls)
			{
				Control component = (Control)obj;
				InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(component)[typeof(InheritanceAttribute)];
				if (inheritanceAttribute != null && inheritanceAttribute.InheritanceLevel != InheritanceLevel.NotInherited)
				{
					flag = true;
				}
				else
				{
					flag2 = true;
				}
				if (flag && flag2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0009C860 File Offset: 0x0009AA60
		protected virtual bool HasSitedNonReadonlyChildren(Control parent)
		{
			if (!parent.HasChildren)
			{
				return false;
			}
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control.Site != null && control.Site.DesignMode)
				{
					InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(control)[typeof(InheritanceAttribute)];
					if (inheritanceAttribute != null && inheritanceAttribute.InheritanceLevel != InheritanceLevel.InheritedReadOnly)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0009C904 File Offset: 0x0009AB04
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			if (manager == null || value == null)
			{
				throw new ArgumentNullException((manager == null) ? "manager" : "value");
			}
			CodeDomSerializer codeDomSerializer = (CodeDomSerializer)manager.GetSerializer(typeof(Component), typeof(CodeDomSerializer));
			if (codeDomSerializer == null)
			{
				return null;
			}
			object obj = codeDomSerializer.Serialize(manager, value);
			InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(value)[typeof(InheritanceAttribute)];
			InheritanceLevel inheritanceLevel = InheritanceLevel.NotInherited;
			if (inheritanceAttribute != null)
			{
				inheritanceLevel = inheritanceAttribute.InheritanceLevel;
			}
			if (inheritanceLevel != InheritanceLevel.InheritedReadOnly)
			{
				IDesignerHost designerHost = (IDesignerHost)manager.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(designerHost.RootComponent)["Localizable"];
					if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(bool) && (bool)propertyDescriptor.GetValue(designerHost.RootComponent))
					{
						this.SerializeControlHierarchy(manager, designerHost, value);
					}
				}
				CodeStatementCollection codeStatementCollection = obj as CodeStatementCollection;
				if (codeStatementCollection != null)
				{
					Control control = (Control)value;
					if ((designerHost != null && control == designerHost.RootComponent) || this.HasSitedNonReadonlyChildren(control))
					{
						this.SerializeSuspendLayout(manager, codeStatementCollection, value);
						this.SerializeResumeLayout(manager, codeStatementCollection, value);
						ControlDesigner controlDesigner = designerHost.GetDesigner(control) as ControlDesigner;
						if (this.HasAutoSizedChildren(control) || (controlDesigner != null && controlDesigner.SerializePerformLayout))
						{
							this.SerializePerformLayout(manager, codeStatementCollection, value);
						}
					}
					if (this.HasMixedInheritedChildren(control))
					{
						this.SerializeZOrder(manager, codeStatementCollection, control);
					}
				}
			}
			return obj;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0009CA80 File Offset: 0x0009AC80
		private void SerializeControlHierarchy(IDesignerSerializationManager manager, IDesignerHost host, object value)
		{
			Control control = value as Control;
			if (control != null)
			{
				IMultitargetHelperService multitargetHelperService = host.GetService(typeof(IMultitargetHelperService)) as IMultitargetHelperService;
				string text;
				if (control == host.RootComponent)
				{
					text = "$this";
					using (IEnumerator enumerator = host.Container.Components.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							IComponent component = (IComponent)obj;
							if (!(component is Control) && !TypeDescriptor.GetAttributes(component).Contains(InheritanceAttribute.InheritedReadOnly))
							{
								string name = manager.GetName(component);
								string value2 = (multitargetHelperService == null) ? component.GetType().AssemblyQualifiedName : multitargetHelperService.GetAssemblyQualifiedName(component.GetType());
								if (name != null)
								{
									base.SerializeResourceInvariant(manager, ">>" + name + ".Name", name);
									base.SerializeResourceInvariant(manager, ">>" + name + ".Type", value2);
								}
							}
						}
						goto IL_107;
					}
				}
				text = manager.GetName(value);
				if (text == null)
				{
					return;
				}
				IL_107:
				base.SerializeResourceInvariant(manager, ">>" + text + ".Name", manager.GetName(value));
				base.SerializeResourceInvariant(manager, ">>" + text + ".Type", (multitargetHelperService == null) ? control.GetType().AssemblyQualifiedName : multitargetHelperService.GetAssemblyQualifiedName(control.GetType()));
				Control parent = control.Parent;
				if (parent != null && parent.Site != null)
				{
					string text2;
					if (parent == host.RootComponent)
					{
						text2 = "$this";
					}
					else
					{
						text2 = manager.GetName(parent);
					}
					if (text2 != null)
					{
						base.SerializeResourceInvariant(manager, ">>" + text + ".Parent", text2);
					}
					for (int i = 0; i < parent.Controls.Count; i++)
					{
						if (parent.Controls[i] == control)
						{
							base.SerializeResourceInvariant(manager, ">>" + text + ".ZOrder", i.ToString(CultureInfo.InvariantCulture));
							return;
						}
					}
				}
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0009CC94 File Offset: 0x0009AE94
		private static Type ToTargetType(object context, Type runtimeType)
		{
			return TypeDescriptor.GetProvider(context).GetReflectionType(runtimeType);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0009CCA4 File Offset: 0x0009AEA4
		private static Type[] ToTargetTypes(object context, Type[] runtimeTypes)
		{
			Type[] array = new Type[runtimeTypes.Length];
			for (int i = 0; i < runtimeTypes.Length; i++)
			{
				array[i] = ControlCodeDomSerializer.ToTargetType(context, runtimeTypes[i]);
			}
			return array;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0009CCD8 File Offset: 0x0009AED8
		private void SerializeMethodInvocation(IDesignerSerializationManager manager, CodeStatementCollection statements, object control, string methodName, CodeExpressionCollection parameters, Type[] paramTypes, ControlCodeDomSerializer.StatementOrdering ordering)
		{
			using (CodeDomSerializerBase.TraceScope("ControlCodeDomSerializer::SerializeMethodInvocation(" + methodName + ")"))
			{
				string name = manager.GetName(control);
				paramTypes = ControlCodeDomSerializer.ToTargetTypes(control, paramTypes);
				MethodInfo method = TypeDescriptor.GetReflectionType(control).GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public, null, paramTypes, null);
				if (method != null)
				{
					CodeExpression targetObject = base.SerializeToExpression(manager, control);
					CodeMethodReferenceExpression method2 = new CodeMethodReferenceExpression(targetObject, methodName);
					CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
					codeMethodInvokeExpression.Method = method2;
					if (parameters != null)
					{
						codeMethodInvokeExpression.Parameters.AddRange(parameters);
					}
					CodeExpressionStatement codeExpressionStatement = new CodeExpressionStatement(codeMethodInvokeExpression);
					if (ordering != ControlCodeDomSerializer.StatementOrdering.Prepend)
					{
						if (ordering == ControlCodeDomSerializer.StatementOrdering.Append)
						{
							codeExpressionStatement.UserData["statement-ordering"] = "end";
						}
					}
					else
					{
						codeExpressionStatement.UserData["statement-ordering"] = "begin";
					}
					statements.Add(codeExpressionStatement);
				}
			}
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0009CDCC File Offset: 0x0009AFCC
		private void SerializePerformLayout(IDesignerSerializationManager manager, CodeStatementCollection statements, object control)
		{
			this.SerializeMethodInvocation(manager, statements, control, "PerformLayout", null, new Type[0], ControlCodeDomSerializer.StatementOrdering.Append);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0009CDE4 File Offset: 0x0009AFE4
		private void SerializeResumeLayout(IDesignerSerializationManager manager, CodeStatementCollection statements, object control)
		{
			CodeExpressionCollection codeExpressionCollection = new CodeExpressionCollection();
			codeExpressionCollection.Add(new CodePrimitiveExpression(false));
			Type[] paramTypes = new Type[]
			{
				typeof(bool)
			};
			this.SerializeMethodInvocation(manager, statements, control, "ResumeLayout", codeExpressionCollection, paramTypes, ControlCodeDomSerializer.StatementOrdering.Append);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0009CE2E File Offset: 0x0009B02E
		private void SerializeSuspendLayout(IDesignerSerializationManager manager, CodeStatementCollection statements, object control)
		{
			this.SerializeMethodInvocation(manager, statements, control, "SuspendLayout", null, new Type[0], ControlCodeDomSerializer.StatementOrdering.Prepend);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0009CE48 File Offset: 0x0009B048
		private void SerializeZOrder(IDesignerSerializationManager manager, CodeStatementCollection statements, Control control)
		{
			using (CodeDomSerializerBase.TraceScope("ControlCodeDomSerializer::SerializeZOrder()"))
			{
				for (int i = control.Controls.Count - 1; i >= 0; i--)
				{
					Control control2 = control.Controls[i];
					if (control2.Site != null && control2.Site.Container == control.Site.Container)
					{
						InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(control2)[typeof(InheritanceAttribute)];
						if (inheritanceAttribute.InheritanceLevel != InheritanceLevel.InheritedReadOnly)
						{
							CodeExpression targetObject = new CodePropertyReferenceExpression(base.SerializeToExpression(manager, control), "Controls");
							CodeMethodReferenceExpression method = new CodeMethodReferenceExpression(targetObject, "SetChildIndex");
							CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
							codeMethodInvokeExpression.Method = method;
							CodeExpression value = base.SerializeToExpression(manager, control2);
							codeMethodInvokeExpression.Parameters.Add(value);
							codeMethodInvokeExpression.Parameters.Add(base.SerializeToExpression(manager, 0));
							CodeExpressionStatement value2 = new CodeExpressionStatement(codeMethodInvokeExpression);
							statements.Add(value2);
						}
					}
				}
			}
		}

		// Token: 0x0200053F RID: 1343
		private enum StatementOrdering
		{
			// Token: 0x0400210F RID: 8463
			Prepend,
			// Token: 0x04002110 RID: 8464
			Append
		}
	}
}
