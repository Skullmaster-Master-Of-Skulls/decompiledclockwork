using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000805 RID: 2053
	internal class TypeNameBuilder
	{
		// Token: 0x060048BB RID: 18619
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr _CreateTypeNameBuilder();

		// Token: 0x060048BC RID: 18620
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _ReleaseTypeNameBuilder(IntPtr pAQN);

		// Token: 0x060048BD RID: 18621
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _OpenGenericArguments(IntPtr tnb);

		// Token: 0x060048BE RID: 18622
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _CloseGenericArguments(IntPtr tnb);

		// Token: 0x060048BF RID: 18623
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _OpenGenericArgument(IntPtr tnb);

		// Token: 0x060048C0 RID: 18624
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _CloseGenericArgument(IntPtr tnb);

		// Token: 0x060048C1 RID: 18625
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _AddName(IntPtr tnb, string name);

		// Token: 0x060048C2 RID: 18626
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _AddPointer(IntPtr tnb);

		// Token: 0x060048C3 RID: 18627
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _AddByRef(IntPtr tnb);

		// Token: 0x060048C4 RID: 18628
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _AddSzArray(IntPtr tnb);

		// Token: 0x060048C5 RID: 18629
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _AddArray(IntPtr tnb, int rank);

		// Token: 0x060048C6 RID: 18630
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _AddAssemblySpec(IntPtr tnb, string assemblySpec);

		// Token: 0x060048C7 RID: 18631
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string _ToString(IntPtr tnb);

		// Token: 0x060048C8 RID: 18632
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _Clear(IntPtr tnb);

		// Token: 0x060048C9 RID: 18633 RVA: 0x000FD610 File Offset: 0x000FC610
		internal static string ToString(Type type, TypeNameBuilder.Format format)
		{
			if ((format == TypeNameBuilder.Format.FullName || format == TypeNameBuilder.Format.AssemblyQualifiedName) && !type.IsGenericTypeDefinition && type.ContainsGenericParameters)
			{
				return null;
			}
			TypeNameBuilder typeNameBuilder = new TypeNameBuilder(TypeNameBuilder._CreateTypeNameBuilder());
			typeNameBuilder.Clear();
			typeNameBuilder.ConstructAssemblyQualifiedNameWorker(type, format);
			string result = typeNameBuilder.ToString();
			typeNameBuilder.Dispose();
			return result;
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x000FD65E File Offset: 0x000FC65E
		private TypeNameBuilder(IntPtr typeNameBuilder)
		{
			this.m_typeNameBuilder = typeNameBuilder;
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x000FD66D File Offset: 0x000FC66D
		internal void Dispose()
		{
			TypeNameBuilder._ReleaseTypeNameBuilder(this.m_typeNameBuilder);
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x000FD67C File Offset: 0x000FC67C
		private void AddElementType(Type elementType)
		{
			if (elementType.HasElementType)
			{
				this.AddElementType(elementType.GetElementType());
			}
			if (elementType.IsPointer)
			{
				this.AddPointer();
				return;
			}
			if (elementType.IsByRef)
			{
				this.AddByRef();
				return;
			}
			if (elementType.IsSzArray)
			{
				this.AddSzArray();
				return;
			}
			if (elementType.IsArray)
			{
				this.AddArray(elementType.GetArrayRank());
			}
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x000FD6E0 File Offset: 0x000FC6E0
		private void ConstructAssemblyQualifiedNameWorker(Type type, TypeNameBuilder.Format format)
		{
			Type type2 = type;
			while (type2.HasElementType)
			{
				type2 = type2.GetElementType();
			}
			List<Type> list = new List<Type>();
			for (Type type3 = type2; type3 != null; type3 = (type3.IsGenericParameter ? null : type3.DeclaringType))
			{
				list.Add(type3);
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				Type type4 = list[i];
				string text = type4.Name;
				if (i == list.Count - 1 && type4.Namespace != null && type4.Namespace.Length != 0)
				{
					text = type4.Namespace + "." + text;
				}
				this.AddName(text);
			}
			if (type2.IsGenericType && (!type2.IsGenericTypeDefinition || format == TypeNameBuilder.Format.ToString))
			{
				Type[] genericArguments = type2.GetGenericArguments();
				this.OpenGenericArguments();
				for (int j = 0; j < genericArguments.Length; j++)
				{
					TypeNameBuilder.Format format2 = (format == TypeNameBuilder.Format.FullName) ? TypeNameBuilder.Format.AssemblyQualifiedName : format;
					this.OpenGenericArgument();
					this.ConstructAssemblyQualifiedNameWorker(genericArguments[j], format2);
					this.CloseGenericArgument();
				}
				this.CloseGenericArguments();
			}
			this.AddElementType(type);
			if (format == TypeNameBuilder.Format.AssemblyQualifiedName)
			{
				this.AddAssemblySpec(type.Module.Assembly.FullName);
			}
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x000FD808 File Offset: 0x000FC808
		private void OpenGenericArguments()
		{
			TypeNameBuilder._OpenGenericArguments(this.m_typeNameBuilder);
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x000FD815 File Offset: 0x000FC815
		private void CloseGenericArguments()
		{
			TypeNameBuilder._CloseGenericArguments(this.m_typeNameBuilder);
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x000FD822 File Offset: 0x000FC822
		private void OpenGenericArgument()
		{
			TypeNameBuilder._OpenGenericArgument(this.m_typeNameBuilder);
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x000FD82F File Offset: 0x000FC82F
		private void CloseGenericArgument()
		{
			TypeNameBuilder._CloseGenericArgument(this.m_typeNameBuilder);
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x000FD83C File Offset: 0x000FC83C
		private void AddName(string name)
		{
			TypeNameBuilder._AddName(this.m_typeNameBuilder, name);
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x000FD84A File Offset: 0x000FC84A
		private void AddPointer()
		{
			TypeNameBuilder._AddPointer(this.m_typeNameBuilder);
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x000FD857 File Offset: 0x000FC857
		private void AddByRef()
		{
			TypeNameBuilder._AddByRef(this.m_typeNameBuilder);
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x000FD864 File Offset: 0x000FC864
		private void AddSzArray()
		{
			TypeNameBuilder._AddSzArray(this.m_typeNameBuilder);
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x000FD871 File Offset: 0x000FC871
		private void AddArray(int rank)
		{
			TypeNameBuilder._AddArray(this.m_typeNameBuilder, rank);
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x000FD87F File Offset: 0x000FC87F
		private void AddAssemblySpec(string assemblySpec)
		{
			TypeNameBuilder._AddAssemblySpec(this.m_typeNameBuilder, assemblySpec);
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x000FD88D File Offset: 0x000FC88D
		public override string ToString()
		{
			return TypeNameBuilder._ToString(this.m_typeNameBuilder);
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x000FD89A File Offset: 0x000FC89A
		private void Clear()
		{
			TypeNameBuilder._Clear(this.m_typeNameBuilder);
		}

		// Token: 0x0400258B RID: 9611
		private IntPtr m_typeNameBuilder;

		// Token: 0x02000806 RID: 2054
		internal enum Format
		{
			// Token: 0x0400258D RID: 9613
			ToString,
			// Token: 0x0400258E RID: 9614
			FullName,
			// Token: 0x0400258F RID: 9615
			AssemblyQualifiedName
		}
	}
}
