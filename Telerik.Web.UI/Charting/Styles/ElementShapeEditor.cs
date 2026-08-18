using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001775 RID: 6005
	internal class ElementShapeEditor : UITypeEditor
	{
		// Token: 0x0600EA3F RID: 59967 RVA: 0x00355C02 File Offset: 0x00353E02
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x0600EA40 RID: 59968 RVA: 0x00355C08 File Offset: 0x00353E08
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			this.shapes = new ArrayList();
			this.editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			ListBox listBox = this.CreateListBox(context, value);
			this.indexChanged = false;
			this.editorService.DropDownControl(listBox);
			if (!this.indexChanged)
			{
				return value;
			}
			if (listBox.SelectedIndex == 0)
			{
				return null;
			}
			if (listBox.SelectedIndex - 1 <= this.shapes.Count - 1)
			{
				object obj = this.shapes[listBox.SelectedIndex - 1];
				Type type = obj as Type;
				if (type != null)
				{
					if (context.Container != null)
					{
						foreach (object obj2 in context.Container.Components)
						{
							IComponent component = (IComponent)obj2;
							if (component.GetType() == type)
							{
								obj = component;
								break;
							}
						}
						if (type != null)
						{
							IDesignerHost designerHost = (IDesignerHost)context.Container;
							obj = designerHost.CreateComponent(type);
						}
					}
					else
					{
						obj = Activator.CreateInstance(type);
					}
				}
				this.shapes.Clear();
				return obj;
			}
			bool flag = true;
			if (context.Container == null && value.GetType() == typeof(CustomShape))
			{
				flag = false;
			}
			if (listBox.SelectedIndex - 1 == this.shapes.Count && flag)
			{
				CustomShape shape = ElementShapeEditor.CreateNewShape(context);
				return ElementShapeEditor.EditPoints(context, shape);
			}
			return ElementShapeEditor.EditPoints(context, (CustomShape)value);
		}

		// Token: 0x0600EA41 RID: 59969 RVA: 0x00355DA8 File Offset: 0x00353FA8
		private void listBox_SelectedValueChanged(object sender, EventArgs e)
		{
			this.indexChanged = true;
			if (this.editorService != null)
			{
				this.editorService.CloseDropDown();
			}
		}

		// Token: 0x0600EA42 RID: 59970 RVA: 0x00355DC4 File Offset: 0x00353FC4
		private ListBox CreateListBox(ITypeDescriptorContext context, object value)
		{
			ListBox listBox = new ListBox();
			listBox.SelectedValueChanged += this.listBox_SelectedValueChanged;
			listBox.Dock = DockStyle.Fill;
			listBox.BorderStyle = BorderStyle.None;
			listBox.ItemHeight = 13;
			listBox.Items.Add("(none)");
			if (context.Container != null)
			{
				ITypeDiscoveryService typeDiscoveryService = (ITypeDiscoveryService)context.GetService(typeof(ITypeDiscoveryService));
				using (IEnumerator enumerator = typeDiscoveryService.GetTypes(typeof(ElementShape), false).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Type type = (Type)obj;
						if (type != typeof(CustomShape) && !type.IsAbstract && type.IsPublic)
						{
							listBox.Items.Add(type.Name);
							this.shapes.Add(type);
							if (value != null && value.GetType() == type)
							{
								listBox.SelectedIndex = listBox.Items.Count - 1;
							}
						}
					}
					goto IL_1FC;
				}
			}
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				try
				{
					foreach (Type type2 in assembly.GetTypes())
					{
						if (type2.IsClass && type2.IsPublic && !type2.IsAbstract && typeof(ElementShape).IsAssignableFrom(type2) && type2 != typeof(CustomShape))
						{
							listBox.Items.Add(type2.Name);
							this.shapes.Add(type2);
							if (value != null && value.GetType() == type2)
							{
								listBox.SelectedIndex = listBox.Items.Count - 1;
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			IL_1FC:
			if (context.Container != null)
			{
				foreach (object obj2 in context.Container.Components)
				{
					IComponent component = (IComponent)obj2;
					if (component is CustomShape)
					{
						listBox.Items.Add(component.Site.Name);
						this.shapes.Add(component);
						if (component == value)
						{
							listBox.SelectedIndex = listBox.Items.Count - 1;
						}
					}
				}
				listBox.Items.Add("Create new custom shape ...");
				if (value != null && value.GetType() == typeof(CustomShape))
				{
					listBox.Items.Add("Edit points ...");
				}
			}
			else if (value != null && value.GetType() == typeof(CustomShape))
			{
				listBox.Items.Add("Edit points ...");
			}
			else
			{
				listBox.Items.Add("Create new custom shape ...");
			}
			return listBox;
		}

		// Token: 0x0600EA43 RID: 59971 RVA: 0x00356108 File Offset: 0x00354308
		private static CustomShape CreateNewShape(ITypeDescriptorContext context)
		{
			CustomShape customShape;
			if (context != null && context.Container != null)
			{
				customShape = (CustomShape)(context.Container as IDesignerHost).CreateComponent(typeof(CustomShape));
			}
			else
			{
				customShape = new CustomShape();
			}
			customShape.Dimension = new Rectangle(20, 20, 200, 100);
			customShape.Points.Add(new ShapePoint(20, 20));
			customShape.Points.Add(new ShapePoint(220, 20));
			customShape.Points.Add(new ShapePoint(220, 120));
			customShape.Points.Add(new ShapePoint(20, 120));
			return customShape;
		}

		// Token: 0x0600EA44 RID: 59972 RVA: 0x003561B8 File Offset: 0x003543B8
		private static CustomShape EditPoints(ITypeDescriptorContext context, CustomShape shape)
		{
			CustomShapeEditorForm customShapeEditorForm = new CustomShapeEditorForm();
			customShapeEditorForm.EditorControl.Dimension = shape.Dimension;
			foreach (ShapePoint point in shape.Points)
			{
				customShapeEditorForm.EditorControl.Points.Add(new ShapePoint(point));
			}
			if (customShapeEditorForm.ShowDialog() == DialogResult.OK)
			{
				IDesignerHost designerHost = context.Container as IDesignerHost;
				if (designerHost != null)
				{
					foreach (ShapePoint component in shape.Points)
					{
						designerHost.DestroyComponent(component);
					}
				}
				shape.Points.Clear();
				foreach (ShapePoint shapePoint in customShapeEditorForm.EditorControl.Points)
				{
					ShapePoint shapePoint2;
					if (designerHost != null)
					{
						shapePoint2 = (ShapePoint)designerHost.CreateComponent(typeof(ShapePoint));
					}
					else
					{
						shapePoint2 = new ShapePoint();
					}
					shapePoint2.X = shapePoint.X;
					shapePoint2.Y = shapePoint.Y;
					shapePoint2.ControlPoint1 = shapePoint.ControlPoint1;
					shapePoint2.ControlPoint2 = shapePoint.ControlPoint2;
					shapePoint2.Bezier = shapePoint.Bezier;
					shapePoint2.Locked = shapePoint.Locked;
					shape.Points.Add(shapePoint2);
				}
				shape.Dimension = customShapeEditorForm.EditorControl.Dimension;
			}
			return shape;
		}

		// Token: 0x04004383 RID: 17283
		private IWindowsFormsEditorService editorService;

		// Token: 0x04004384 RID: 17284
		private ArrayList shapes;

		// Token: 0x04004385 RID: 17285
		private bool indexChanged;
	}
}
