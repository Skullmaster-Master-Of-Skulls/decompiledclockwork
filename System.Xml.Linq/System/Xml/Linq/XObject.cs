using System;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	// Token: 0x02000010 RID: 16
	[__DynamicallyInvokable]
	public abstract class XObject : IXmlLineInfo
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00004104 File Offset: 0x00002304
		internal XObject()
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000410C File Offset: 0x0000230C
		[__DynamicallyInvokable]
		public string BaseUri
		{
			[__DynamicallyInvokable]
			get
			{
				XObject xobject = this;
				BaseUriAnnotation baseUriAnnotation;
				for (;;)
				{
					if (xobject == null || xobject.annotations != null)
					{
						if (xobject == null)
						{
							goto IL_33;
						}
						baseUriAnnotation = xobject.Annotation<BaseUriAnnotation>();
						if (baseUriAnnotation != null)
						{
							break;
						}
						xobject = xobject.parent;
					}
					else
					{
						xobject = xobject.parent;
					}
				}
				return baseUriAnnotation.baseUri;
				IL_33:
				return string.Empty;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004154 File Offset: 0x00002354
		[__DynamicallyInvokable]
		public XDocument Document
		{
			[__DynamicallyInvokable]
			get
			{
				XObject xobject = this;
				while (xobject.parent != null)
				{
					xobject = xobject.parent;
				}
				return xobject as XDocument;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000086 RID: 134
		[__DynamicallyInvokable]
		public abstract XmlNodeType NodeType { [__DynamicallyInvokable] get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000417A File Offset: 0x0000237A
		[__DynamicallyInvokable]
		public XElement Parent
		{
			[__DynamicallyInvokable]
			get
			{
				return this.parent as XElement;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004188 File Offset: 0x00002388
		[__DynamicallyInvokable]
		public void AddAnnotation(object annotation)
		{
			if (annotation == null)
			{
				throw new ArgumentNullException("annotation");
			}
			if (this.annotations == null)
			{
				object obj;
				if (!(annotation is object[]))
				{
					obj = annotation;
				}
				else
				{
					(obj = new object[1])[0] = annotation;
				}
				this.annotations = obj;
				return;
			}
			object[] array = this.annotations as object[];
			if (array == null)
			{
				this.annotations = new object[]
				{
					this.annotations,
					annotation
				};
				return;
			}
			int num = 0;
			while (num < array.Length && array[num] != null)
			{
				num++;
			}
			if (num == array.Length)
			{
				Array.Resize<object>(ref array, num * 2);
				this.annotations = array;
			}
			array[num] = annotation;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004220 File Offset: 0x00002420
		[__DynamicallyInvokable]
		public object Annotation(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					if (type.IsInstanceOfType(this.annotations))
					{
						return this.annotations;
					}
				}
				else
				{
					foreach (object obj in array)
					{
						if (obj == null)
						{
							break;
						}
						if (type.IsInstanceOfType(obj))
						{
							return obj;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004290 File Offset: 0x00002490
		[__DynamicallyInvokable]
		public T Annotation<T>() where T : class
		{
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					return this.annotations as T;
				}
				foreach (object obj in array)
				{
					if (obj == null)
					{
						break;
					}
					T t = obj as T;
					if (t != null)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000042FA File Offset: 0x000024FA
		[__DynamicallyInvokable]
		public IEnumerable<object> Annotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this.AnnotationsIterator(type);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004317 File Offset: 0x00002517
		private IEnumerable<object> AnnotationsIterator(Type type)
		{
			if (this.annotations != null)
			{
				object[] a = this.annotations as object[];
				if (a == null)
				{
					if (type.IsInstanceOfType(this.annotations))
					{
						yield return this.annotations;
					}
				}
				else
				{
					int num;
					for (int i = 0; i < a.Length; i = num + 1)
					{
						object obj = a[i];
						if (obj == null)
						{
							break;
						}
						if (type.IsInstanceOfType(obj))
						{
							yield return obj;
						}
						num = i;
					}
				}
				a = null;
			}
			yield break;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000432E File Offset: 0x0000252E
		[__DynamicallyInvokable]
		public IEnumerable<T> Annotations<T>() where T : class
		{
			if (this.annotations != null)
			{
				object[] a = this.annotations as object[];
				if (a == null)
				{
					T t = this.annotations as T;
					if (t != null)
					{
						yield return t;
					}
				}
				else
				{
					int num;
					for (int i = 0; i < a.Length; i = num + 1)
					{
						object obj = a[i];
						if (obj == null)
						{
							break;
						}
						T t2 = obj as T;
						if (t2 != null)
						{
							yield return t2;
						}
						num = i;
					}
				}
				a = null;
			}
			yield break;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004340 File Offset: 0x00002540
		[__DynamicallyInvokable]
		public void RemoveAnnotations(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					if (type.IsInstanceOfType(this.annotations))
					{
						this.annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!type.IsInstanceOfType(obj))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j == 0)
					{
						this.annotations = null;
						return;
					}
					while (j < i)
					{
						array[j++] = null;
					}
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000043D0 File Offset: 0x000025D0
		[__DynamicallyInvokable]
		public void RemoveAnnotations<T>() where T : class
		{
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					if (this.annotations is T)
					{
						this.annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int j = 0;
					while (i < array.Length)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (!(obj is T))
						{
							array[j++] = obj;
						}
						i++;
					}
					if (j == 0)
					{
						this.annotations = null;
						return;
					}
					while (j < i)
					{
						array[j++] = null;
					}
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000090 RID: 144 RVA: 0x00004448 File Offset: 0x00002648
		// (remove) Token: 0x06000091 RID: 145 RVA: 0x00004488 File Offset: 0x00002688
		[__DynamicallyInvokable]
		public event EventHandler<XObjectChangeEventArgs> Changed
		{
			[__DynamicallyInvokable]
			add
			{
				if (value == null)
				{
					return;
				}
				XObjectChangeAnnotation xobjectChangeAnnotation = this.Annotation<XObjectChangeAnnotation>();
				if (xobjectChangeAnnotation == null)
				{
					xobjectChangeAnnotation = new XObjectChangeAnnotation();
					this.AddAnnotation(xobjectChangeAnnotation);
				}
				XObjectChangeAnnotation xobjectChangeAnnotation2 = xobjectChangeAnnotation;
				xobjectChangeAnnotation2.changed = (EventHandler<XObjectChangeEventArgs>)Delegate.Combine(xobjectChangeAnnotation2.changed, value);
			}
			[__DynamicallyInvokable]
			remove
			{
				if (value == null)
				{
					return;
				}
				XObjectChangeAnnotation xobjectChangeAnnotation = this.Annotation<XObjectChangeAnnotation>();
				if (xobjectChangeAnnotation == null)
				{
					return;
				}
				XObjectChangeAnnotation xobjectChangeAnnotation2 = xobjectChangeAnnotation;
				xobjectChangeAnnotation2.changed = (EventHandler<XObjectChangeEventArgs>)Delegate.Remove(xobjectChangeAnnotation2.changed, value);
				if (xobjectChangeAnnotation.changing == null && xobjectChangeAnnotation.changed == null)
				{
					this.RemoveAnnotations<XObjectChangeAnnotation>();
				}
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000092 RID: 146 RVA: 0x000044D4 File Offset: 0x000026D4
		// (remove) Token: 0x06000093 RID: 147 RVA: 0x00004514 File Offset: 0x00002714
		[__DynamicallyInvokable]
		public event EventHandler<XObjectChangeEventArgs> Changing
		{
			[__DynamicallyInvokable]
			add
			{
				if (value == null)
				{
					return;
				}
				XObjectChangeAnnotation xobjectChangeAnnotation = this.Annotation<XObjectChangeAnnotation>();
				if (xobjectChangeAnnotation == null)
				{
					xobjectChangeAnnotation = new XObjectChangeAnnotation();
					this.AddAnnotation(xobjectChangeAnnotation);
				}
				XObjectChangeAnnotation xobjectChangeAnnotation2 = xobjectChangeAnnotation;
				xobjectChangeAnnotation2.changing = (EventHandler<XObjectChangeEventArgs>)Delegate.Combine(xobjectChangeAnnotation2.changing, value);
			}
			[__DynamicallyInvokable]
			remove
			{
				if (value == null)
				{
					return;
				}
				XObjectChangeAnnotation xobjectChangeAnnotation = this.Annotation<XObjectChangeAnnotation>();
				if (xobjectChangeAnnotation == null)
				{
					return;
				}
				XObjectChangeAnnotation xobjectChangeAnnotation2 = xobjectChangeAnnotation;
				xobjectChangeAnnotation2.changing = (EventHandler<XObjectChangeEventArgs>)Delegate.Remove(xobjectChangeAnnotation2.changing, value);
				if (xobjectChangeAnnotation.changing == null && xobjectChangeAnnotation.changed == null)
				{
					this.RemoveAnnotations<XObjectChangeAnnotation>();
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000455D File Offset: 0x0000275D
		[__DynamicallyInvokable]
		bool IXmlLineInfo.HasLineInfo()
		{
			return this.Annotation<LineInfoAnnotation>() != null;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004568 File Offset: 0x00002768
		[__DynamicallyInvokable]
		int IXmlLineInfo.LineNumber
		{
			[__DynamicallyInvokable]
			get
			{
				LineInfoAnnotation lineInfoAnnotation = this.Annotation<LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.lineNumber;
				}
				return 0;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004588 File Offset: 0x00002788
		[__DynamicallyInvokable]
		int IXmlLineInfo.LinePosition
		{
			[__DynamicallyInvokable]
			get
			{
				LineInfoAnnotation lineInfoAnnotation = this.Annotation<LineInfoAnnotation>();
				if (lineInfoAnnotation != null)
				{
					return lineInfoAnnotation.linePosition;
				}
				return 0;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000045A7 File Offset: 0x000027A7
		internal bool HasBaseUri
		{
			get
			{
				return this.Annotation<BaseUriAnnotation>() != null;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000045B4 File Offset: 0x000027B4
		internal bool NotifyChanged(object sender, XObjectChangeEventArgs e)
		{
			bool result = false;
			XObject xobject = this;
			for (;;)
			{
				if (xobject == null || xobject.annotations != null)
				{
					if (xobject == null)
					{
						break;
					}
					XObjectChangeAnnotation xobjectChangeAnnotation = xobject.Annotation<XObjectChangeAnnotation>();
					if (xobjectChangeAnnotation != null)
					{
						result = true;
						if (xobjectChangeAnnotation.changed != null)
						{
							xobjectChangeAnnotation.changed(sender, e);
						}
					}
					xobject = xobject.parent;
				}
				else
				{
					xobject = xobject.parent;
				}
			}
			return result;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004608 File Offset: 0x00002808
		internal bool NotifyChanging(object sender, XObjectChangeEventArgs e)
		{
			bool result = false;
			XObject xobject = this;
			for (;;)
			{
				if (xobject == null || xobject.annotations != null)
				{
					if (xobject == null)
					{
						break;
					}
					XObjectChangeAnnotation xobjectChangeAnnotation = xobject.Annotation<XObjectChangeAnnotation>();
					if (xobjectChangeAnnotation != null)
					{
						result = true;
						if (xobjectChangeAnnotation.changing != null)
						{
							xobjectChangeAnnotation.changing(sender, e);
						}
					}
					xobject = xobject.parent;
				}
				else
				{
					xobject = xobject.parent;
				}
			}
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000465B File Offset: 0x0000285B
		internal void SetBaseUri(string baseUri)
		{
			this.AddAnnotation(new BaseUriAnnotation(baseUri));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004669 File Offset: 0x00002869
		internal void SetLineInfo(int lineNumber, int linePosition)
		{
			this.AddAnnotation(new LineInfoAnnotation(lineNumber, linePosition));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004678 File Offset: 0x00002878
		internal bool SkipNotify()
		{
			XObject xobject = this;
			for (;;)
			{
				if (xobject == null || xobject.annotations != null)
				{
					if (xobject == null)
					{
						break;
					}
					if (xobject.Annotations<XObjectChangeAnnotation>() != null)
					{
						return false;
					}
					xobject = xobject.parent;
				}
				else
				{
					xobject = xobject.parent;
				}
			}
			return true;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000046B4 File Offset: 0x000028B4
		internal SaveOptions GetSaveOptionsFromAnnotations()
		{
			XObject xobject = this;
			object obj;
			for (;;)
			{
				if (xobject == null || xobject.annotations != null)
				{
					if (xobject == null)
					{
						break;
					}
					obj = xobject.Annotation(typeof(SaveOptions));
					if (obj != null)
					{
						goto Block_3;
					}
					xobject = xobject.parent;
				}
				else
				{
					xobject = xobject.parent;
				}
			}
			return SaveOptions.None;
			Block_3:
			return (SaveOptions)obj;
		}

		// Token: 0x04000072 RID: 114
		internal XContainer parent;

		// Token: 0x04000073 RID: 115
		internal object annotations;
	}
}
