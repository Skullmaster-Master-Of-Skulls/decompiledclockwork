using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Accessibility;

namespace System.Windows.Forms
{
	// Token: 0x0200011A RID: 282
	internal sealed class InternalAccessibleObject : StandardOleMarshalObject, UnsafeNativeMethods.IAccessibleInternal, IReflect, UnsafeNativeMethods.IServiceProvider, UnsafeNativeMethods.IAccessibleEx, UnsafeNativeMethods.IRawElementProviderSimple, UnsafeNativeMethods.IRawElementProviderFragment, UnsafeNativeMethods.IRawElementProviderFragmentRoot, UnsafeNativeMethods.IInvokeProvider, UnsafeNativeMethods.IValueProvider, UnsafeNativeMethods.IRangeValueProvider, UnsafeNativeMethods.IExpandCollapseProvider, UnsafeNativeMethods.IToggleProvider, UnsafeNativeMethods.ITableProvider, UnsafeNativeMethods.ITableItemProvider, UnsafeNativeMethods.IGridProvider, UnsafeNativeMethods.IGridItemProvider, UnsafeNativeMethods.IEnumVariant, UnsafeNativeMethods.IOleWindow, UnsafeNativeMethods.ILegacyIAccessibleProvider, UnsafeNativeMethods.ISelectionProvider, UnsafeNativeMethods.ISelectionItemProvider, UnsafeNativeMethods.IScrollItemProvider, UnsafeNativeMethods.IRawElementProviderHwndOverride, UnsafeNativeMethods.UiaCore.ITextProvider, UnsafeNativeMethods.UiaCore.ITextProvider2
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x00017624 File Offset: 0x00015824
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal InternalAccessibleObject(AccessibleObject accessibleImplemention)
		{
			this.publicIAccessible = accessibleImplemention;
			this.publicIEnumVariant = accessibleImplemention;
			this.publicIOleWindow = accessibleImplemention;
			this.publicIReflect = accessibleImplemention;
			this.publicIServiceProvider = accessibleImplemention;
			this.publicIAccessibleEx = accessibleImplemention;
			this.publicIRawElementProviderSimple = accessibleImplemention;
			this.publicIRawElementProviderFragment = accessibleImplemention;
			this.publicIRawElementProviderFragmentRoot = accessibleImplemention;
			this.publicIInvokeProvider = accessibleImplemention;
			this.publicIValueProvider = accessibleImplemention;
			this.publicIRangeValueProvider = accessibleImplemention;
			this.publicIExpandCollapseProvider = accessibleImplemention;
			this.publicIToggleProvider = accessibleImplemention;
			this.publicITableProvider = accessibleImplemention;
			this.publicITableItemProvider = accessibleImplemention;
			this.publicIGridProvider = accessibleImplemention;
			this.publicIGridItemProvider = accessibleImplemention;
			this.publicILegacyIAccessibleProvider = accessibleImplemention;
			this.publicISelectionProvider = accessibleImplemention;
			this.publicISelectionItemProvider = accessibleImplemention;
			this.publicIScrollItemProvider = accessibleImplemention;
			this.publicIRawElementProviderHwndOverride = accessibleImplemention;
			this.publicITextProvider2 = accessibleImplemention;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000176DF File Offset: 0x000158DF
		private object AsNativeAccessible(object accObject)
		{
			if (accObject is AccessibleObject)
			{
				return new InternalAccessibleObject(accObject as AccessibleObject);
			}
			return accObject;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000176F8 File Offset: 0x000158F8
		private object[] AsArrayOfNativeAccessibles(object[] accObjectArray)
		{
			if (accObjectArray != null && accObjectArray.Length != 0)
			{
				for (int i = 0; i < accObjectArray.Length; i++)
				{
					accObjectArray[i] = this.AsNativeAccessible(accObjectArray[i]);
				}
			}
			return accObjectArray;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00017727 File Offset: 0x00015927
		void UnsafeNativeMethods.IAccessibleInternal.accDoDefaultAction(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIAccessible.accDoDefaultAction(childID);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001773F File Offset: 0x0001593F
		object UnsafeNativeMethods.IAccessibleInternal.accHitTest(int xLeft, int yTop)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIAccessible.accHitTest(xLeft, yTop));
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001775E File Offset: 0x0001595E
		void UnsafeNativeMethods.IAccessibleInternal.accLocation(out int l, out int t, out int w, out int h, object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIAccessible.accLocation(out l, out t, out w, out h, childID);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001777C File Offset: 0x0001597C
		object UnsafeNativeMethods.IAccessibleInternal.accNavigate(int navDir, object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIAccessible.accNavigate(navDir, childID));
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001779B File Offset: 0x0001599B
		void UnsafeNativeMethods.IAccessibleInternal.accSelect(int flagsSelect, object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIAccessible.accSelect(flagsSelect, childID);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000177B4 File Offset: 0x000159B4
		object UnsafeNativeMethods.IAccessibleInternal.get_accChild(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIAccessible.get_accChild(childID));
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000177D2 File Offset: 0x000159D2
		int UnsafeNativeMethods.IAccessibleInternal.get_accChildCount()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.accChildCount;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000177E9 File Offset: 0x000159E9
		string UnsafeNativeMethods.IAccessibleInternal.get_accDefaultAction(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accDefaultAction(childID);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00017801 File Offset: 0x00015A01
		string UnsafeNativeMethods.IAccessibleInternal.get_accDescription(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accDescription(childID);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00017819 File Offset: 0x00015A19
		object UnsafeNativeMethods.IAccessibleInternal.get_accFocus()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIAccessible.accFocus);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00017836 File Offset: 0x00015A36
		string UnsafeNativeMethods.IAccessibleInternal.get_accHelp(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accHelp(childID);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001784E File Offset: 0x00015A4E
		int UnsafeNativeMethods.IAccessibleInternal.get_accHelpTopic(out string pszHelpFile, object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accHelpTopic(out pszHelpFile, childID);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00017867 File Offset: 0x00015A67
		string UnsafeNativeMethods.IAccessibleInternal.get_accKeyboardShortcut(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accKeyboardShortcut(childID);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001787F File Offset: 0x00015A7F
		string UnsafeNativeMethods.IAccessibleInternal.get_accName(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accName(childID);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00017897 File Offset: 0x00015A97
		object UnsafeNativeMethods.IAccessibleInternal.get_accParent()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIAccessible.accParent);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000178B4 File Offset: 0x00015AB4
		object UnsafeNativeMethods.IAccessibleInternal.get_accRole(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accRole(childID);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000178CC File Offset: 0x00015ACC
		object UnsafeNativeMethods.IAccessibleInternal.get_accSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIAccessible.accSelection);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x000178E9 File Offset: 0x00015AE9
		object UnsafeNativeMethods.IAccessibleInternal.get_accState(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accState(childID);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00017901 File Offset: 0x00015B01
		string UnsafeNativeMethods.IAccessibleInternal.get_accValue(object childID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessible.get_accValue(childID);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00017919 File Offset: 0x00015B19
		void UnsafeNativeMethods.IAccessibleInternal.set_accName(object childID, string newName)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIAccessible.set_accName(childID, newName);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00017932 File Offset: 0x00015B32
		void UnsafeNativeMethods.IAccessibleInternal.set_accValue(object childID, string newValue)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIAccessible.set_accValue(childID, newValue);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001794B File Offset: 0x00015B4B
		void UnsafeNativeMethods.IEnumVariant.Clone(UnsafeNativeMethods.IEnumVariant[] v)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIEnumVariant.Clone(v);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00017963 File Offset: 0x00015B63
		int UnsafeNativeMethods.IEnumVariant.Next(int n, IntPtr rgvar, int[] ns)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIEnumVariant.Next(n, rgvar, ns);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001797D File Offset: 0x00015B7D
		void UnsafeNativeMethods.IEnumVariant.Reset()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIEnumVariant.Reset();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00017994 File Offset: 0x00015B94
		void UnsafeNativeMethods.IEnumVariant.Skip(int n)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIEnumVariant.Skip(n);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x000179AC File Offset: 0x00015BAC
		int UnsafeNativeMethods.IOleWindow.GetWindow(out IntPtr hwnd)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIOleWindow.GetWindow(out hwnd);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000179C4 File Offset: 0x00015BC4
		void UnsafeNativeMethods.IOleWindow.ContextSensitiveHelp(int fEnterMode)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIOleWindow.ContextSensitiveHelp(fEnterMode);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000179DC File Offset: 0x00015BDC
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.publicIReflect.GetMethod(name, bindingAttr, binder, types, modifiers);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000179F0 File Offset: 0x00015BF0
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetMethod(name, bindingAttr);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000179FF File Offset: 0x00015BFF
		MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetMethods(bindingAttr);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00017A0D File Offset: 0x00015C0D
		FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetField(name, bindingAttr);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00017A1C File Offset: 0x00015C1C
		FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetFields(bindingAttr);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00017A2A File Offset: 0x00015C2A
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetProperty(name, bindingAttr);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00017A39 File Offset: 0x00015C39
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return this.publicIReflect.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00017A4F File Offset: 0x00015C4F
		PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetProperties(bindingAttr);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00017A5D File Offset: 0x00015C5D
		MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetMember(name, bindingAttr);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00017A6C File Offset: 0x00015C6C
		MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
		{
			return this.publicIReflect.GetMembers(bindingAttr);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00017A7C File Offset: 0x00015C7C
		object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			IntSecurity.UnmanagedCode.Demand();
			return this.publicIReflect.InvokeMember(name, invokeAttr, binder, this.publicIAccessible, args, modifiers, culture, namedParameters);
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x00017AB0 File Offset: 0x00015CB0
		Type IReflect.UnderlyingSystemType
		{
			get
			{
				IReflect reflect = this.publicIReflect;
				return this.publicIReflect.UnderlyingSystemType;
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00017AD0 File Offset: 0x00015CD0
		int UnsafeNativeMethods.IServiceProvider.QueryService(ref Guid service, ref Guid riid, out IntPtr ppvObject)
		{
			IntSecurity.UnmanagedCode.Assert();
			ppvObject = IntPtr.Zero;
			int num = this.publicIServiceProvider.QueryService(ref service, ref riid, out ppvObject);
			if (num >= 0)
			{
				ppvObject = Marshal.GetComInterfaceForObject(this, typeof(UnsafeNativeMethods.IAccessibleEx));
			}
			return num;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00017B14 File Offset: 0x00015D14
		object UnsafeNativeMethods.IAccessibleEx.GetObjectForChild(int idChild)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessibleEx.GetObjectForChild(idChild);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00017B2C File Offset: 0x00015D2C
		int UnsafeNativeMethods.IAccessibleEx.GetIAccessiblePair(out object ppAcc, out int pidChild)
		{
			IntSecurity.UnmanagedCode.Assert();
			ppAcc = this;
			pidChild = 0;
			return 0;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00017B3F File Offset: 0x00015D3F
		int[] UnsafeNativeMethods.IAccessibleEx.GetRuntimeId()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessibleEx.GetRuntimeId();
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00017B56 File Offset: 0x00015D56
		int UnsafeNativeMethods.IAccessibleEx.ConvertReturnedElement(object pIn, out object ppRetValOut)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIAccessibleEx.ConvertReturnedElement(pIn, out ppRetValOut);
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00017B6F File Offset: 0x00015D6F
		UnsafeNativeMethods.ProviderOptions UnsafeNativeMethods.IRawElementProviderSimple.ProviderOptions
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRawElementProviderSimple.ProviderOptions;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00017B86 File Offset: 0x00015D86
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.IRawElementProviderSimple.HostRawElementProvider
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRawElementProviderSimple.HostRawElementProvider;
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00017BA0 File Offset: 0x00015DA0
		object UnsafeNativeMethods.IRawElementProviderSimple.GetPatternProvider(int patternId)
		{
			IntSecurity.UnmanagedCode.Assert();
			object patternProvider = this.publicIRawElementProviderSimple.GetPatternProvider(patternId);
			if (patternProvider == null)
			{
				return null;
			}
			if (patternId == 10005)
			{
				return this;
			}
			if (patternId == 10002)
			{
				return this;
			}
			if (AccessibilityImprovements.Level3 && patternId == 10003)
			{
				return this;
			}
			if (patternId == 10015)
			{
				return this;
			}
			if (patternId == 10012)
			{
				return this;
			}
			if (patternId == 10013)
			{
				return this;
			}
			if (patternId == 10006)
			{
				return this;
			}
			if (patternId == 10007)
			{
				return this;
			}
			if (AccessibilityImprovements.Level3 && patternId == 10000)
			{
				return this;
			}
			if (AccessibilityImprovements.Level3 && patternId == 10018)
			{
				return this;
			}
			if (AccessibilityImprovements.Level3 && patternId == 10001)
			{
				return this;
			}
			if (AccessibilityImprovements.Level3 && patternId == 10010)
			{
				return this;
			}
			if (AccessibilityImprovements.Level3 && patternId == 10017)
			{
				return this;
			}
			if (AccessibilityImprovements.Level5 && patternId == 10014)
			{
				return this;
			}
			if (AccessibilityImprovements.Level5 && patternId == 10024)
			{
				return this;
			}
			return null;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00017C9B File Offset: 0x00015E9B
		object UnsafeNativeMethods.IRawElementProviderSimple.GetPropertyValue(int propertyID)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIRawElementProviderSimple.GetPropertyValue(propertyID);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00017CB3 File Offset: 0x00015EB3
		object UnsafeNativeMethods.IRawElementProviderFragment.Navigate(UnsafeNativeMethods.NavigateDirection direction)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIRawElementProviderFragment.Navigate(direction));
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00017CD1 File Offset: 0x00015ED1
		int[] UnsafeNativeMethods.IRawElementProviderFragment.GetRuntimeId()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIRawElementProviderFragment.GetRuntimeId();
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00017CE8 File Offset: 0x00015EE8
		object[] UnsafeNativeMethods.IRawElementProviderFragment.GetEmbeddedFragmentRoots()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsArrayOfNativeAccessibles(this.publicIRawElementProviderFragment.GetEmbeddedFragmentRoots());
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00017D05 File Offset: 0x00015F05
		void UnsafeNativeMethods.IRawElementProviderFragment.SetFocus()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIRawElementProviderFragment.SetFocus();
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00017D1C File Offset: 0x00015F1C
		NativeMethods.UiaRect UnsafeNativeMethods.IRawElementProviderFragment.BoundingRectangle
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRawElementProviderFragment.BoundingRectangle;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00017D33 File Offset: 0x00015F33
		UnsafeNativeMethods.IRawElementProviderFragmentRoot UnsafeNativeMethods.IRawElementProviderFragment.FragmentRoot
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				if (AccessibilityImprovements.Level3)
				{
					return this.publicIRawElementProviderFragment.FragmentRoot;
				}
				return this.AsNativeAccessible(this.publicIRawElementProviderFragment.FragmentRoot) as UnsafeNativeMethods.IRawElementProviderFragmentRoot;
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00017D68 File Offset: 0x00015F68
		object UnsafeNativeMethods.IRawElementProviderFragmentRoot.ElementProviderFromPoint(double x, double y)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIRawElementProviderFragmentRoot.ElementProviderFromPoint(x, y));
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00017D87 File Offset: 0x00015F87
		object UnsafeNativeMethods.IRawElementProviderFragmentRoot.GetFocus()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIRawElementProviderFragmentRoot.GetFocus());
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00017DA4 File Offset: 0x00015FA4
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.DefaultAction
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.DefaultAction;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00017DBB File Offset: 0x00015FBB
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Description
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.Description;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00017DD2 File Offset: 0x00015FD2
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Help
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.Help;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00017DE9 File Offset: 0x00015FE9
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.KeyboardShortcut
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.KeyboardShortcut;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00017E00 File Offset: 0x00016000
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Name
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.Name;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00017E17 File Offset: 0x00016017
		uint UnsafeNativeMethods.ILegacyIAccessibleProvider.Role
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.Role;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00017E2E File Offset: 0x0001602E
		uint UnsafeNativeMethods.ILegacyIAccessibleProvider.State
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.State;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00017E45 File Offset: 0x00016045
		string UnsafeNativeMethods.ILegacyIAccessibleProvider.Value
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.Value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00017E5C File Offset: 0x0001605C
		int UnsafeNativeMethods.ILegacyIAccessibleProvider.ChildId
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicILegacyIAccessibleProvider.ChildId;
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00017E73 File Offset: 0x00016073
		void UnsafeNativeMethods.ILegacyIAccessibleProvider.DoDefaultAction()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicILegacyIAccessibleProvider.DoDefaultAction();
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00017E8A File Offset: 0x0001608A
		IAccessible UnsafeNativeMethods.ILegacyIAccessibleProvider.GetIAccessible()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicILegacyIAccessibleProvider.GetIAccessible();
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00017EA1 File Offset: 0x000160A1
		object[] UnsafeNativeMethods.ILegacyIAccessibleProvider.GetSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsArrayOfNativeAccessibles(this.publicILegacyIAccessibleProvider.GetSelection());
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00017EBE File Offset: 0x000160BE
		void UnsafeNativeMethods.ILegacyIAccessibleProvider.Select(int flagsSelect)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicILegacyIAccessibleProvider.Select(flagsSelect);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00017ED6 File Offset: 0x000160D6
		void UnsafeNativeMethods.ILegacyIAccessibleProvider.SetValue(string szValue)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicILegacyIAccessibleProvider.SetValue(szValue);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00017EEE File Offset: 0x000160EE
		void UnsafeNativeMethods.IInvokeProvider.Invoke()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIInvokeProvider.Invoke();
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00017F05 File Offset: 0x00016105
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider.GetSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.GetSelection();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00017F1C File Offset: 0x0001611C
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider.GetVisibleRanges()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.GetVisibleRanges();
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00017F33 File Offset: 0x00016133
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider.RangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.RangeFromChild(childElement);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00017F4B File Offset: 0x0001614B
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider.RangeFromPoint(Point screenLocation)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.RangeFromPoint(screenLocation);
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00017F63 File Offset: 0x00016163
		UnsafeNativeMethods.UiaCore.SupportedTextSelection UnsafeNativeMethods.UiaCore.ITextProvider.SupportedTextSelection
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicITextProvider2.SupportedTextSelection;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00017F7A File Offset: 0x0001617A
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider.DocumentRange
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicITextProvider2.DocumentRange;
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00017F05 File Offset: 0x00016105
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider2.GetSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.GetSelection();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00017F1C File Offset: 0x0001611C
		UnsafeNativeMethods.UiaCore.ITextRangeProvider[] UnsafeNativeMethods.UiaCore.ITextProvider2.GetVisibleRanges()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.GetVisibleRanges();
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00017F33 File Offset: 0x00016133
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.RangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.RangeFromChild(childElement);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00017F4B File Offset: 0x0001614B
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.RangeFromPoint(Point screenLocation)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.RangeFromPoint(screenLocation);
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00017F63 File Offset: 0x00016163
		UnsafeNativeMethods.UiaCore.SupportedTextSelection UnsafeNativeMethods.UiaCore.ITextProvider2.SupportedTextSelection
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicITextProvider2.SupportedTextSelection;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00017F7A File Offset: 0x0001617A
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.DocumentRange
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicITextProvider2.DocumentRange;
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00017F91 File Offset: 0x00016191
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.GetCaretRange(out UnsafeNativeMethods.BOOL isActive)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.GetCaretRange(out isActive);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00017FA9 File Offset: 0x000161A9
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextProvider2.RangeFromAnnotation(UnsafeNativeMethods.IRawElementProviderSimple annotationElement)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicITextProvider2.RangeFromAnnotation(annotationElement);
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00017FC1 File Offset: 0x000161C1
		bool UnsafeNativeMethods.IValueProvider.IsReadOnly
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIValueProvider.IsReadOnly;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00017FD8 File Offset: 0x000161D8
		string UnsafeNativeMethods.IValueProvider.Value
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIValueProvider.Value;
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00017FEF File Offset: 0x000161EF
		void UnsafeNativeMethods.IValueProvider.SetValue(string newValue)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIValueProvider.SetValue(newValue);
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00017FC1 File Offset: 0x000161C1
		bool UnsafeNativeMethods.IRangeValueProvider.IsReadOnly
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIValueProvider.IsReadOnly;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00018007 File Offset: 0x00016207
		double UnsafeNativeMethods.IRangeValueProvider.LargeChange
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRangeValueProvider.LargeChange;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0001801E File Offset: 0x0001621E
		double UnsafeNativeMethods.IRangeValueProvider.Maximum
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRangeValueProvider.Maximum;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00018035 File Offset: 0x00016235
		double UnsafeNativeMethods.IRangeValueProvider.Minimum
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRangeValueProvider.Minimum;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0001804C File Offset: 0x0001624C
		double UnsafeNativeMethods.IRangeValueProvider.SmallChange
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRangeValueProvider.SmallChange;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00018063 File Offset: 0x00016263
		double UnsafeNativeMethods.IRangeValueProvider.Value
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIRangeValueProvider.Value;
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001807A File Offset: 0x0001627A
		void UnsafeNativeMethods.IRangeValueProvider.SetValue(double newValue)
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIRangeValueProvider.SetValue(newValue);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00018092 File Offset: 0x00016292
		void UnsafeNativeMethods.IExpandCollapseProvider.Expand()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIExpandCollapseProvider.Expand();
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000180A9 File Offset: 0x000162A9
		void UnsafeNativeMethods.IExpandCollapseProvider.Collapse()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIExpandCollapseProvider.Collapse();
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x000180C0 File Offset: 0x000162C0
		UnsafeNativeMethods.ExpandCollapseState UnsafeNativeMethods.IExpandCollapseProvider.ExpandCollapseState
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIExpandCollapseProvider.ExpandCollapseState;
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x000180D7 File Offset: 0x000162D7
		void UnsafeNativeMethods.IToggleProvider.Toggle()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIToggleProvider.Toggle();
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x000180EE File Offset: 0x000162EE
		UnsafeNativeMethods.ToggleState UnsafeNativeMethods.IToggleProvider.ToggleState
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIToggleProvider.ToggleState;
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00018105 File Offset: 0x00016305
		object[] UnsafeNativeMethods.ITableProvider.GetRowHeaders()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsArrayOfNativeAccessibles(this.publicITableProvider.GetRowHeaders());
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00018122 File Offset: 0x00016322
		object[] UnsafeNativeMethods.ITableProvider.GetColumnHeaders()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsArrayOfNativeAccessibles(this.publicITableProvider.GetColumnHeaders());
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0001813F File Offset: 0x0001633F
		UnsafeNativeMethods.RowOrColumnMajor UnsafeNativeMethods.ITableProvider.RowOrColumnMajor
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicITableProvider.RowOrColumnMajor;
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00018156 File Offset: 0x00016356
		object[] UnsafeNativeMethods.ITableItemProvider.GetRowHeaderItems()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsArrayOfNativeAccessibles(this.publicITableItemProvider.GetRowHeaderItems());
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00018173 File Offset: 0x00016373
		object[] UnsafeNativeMethods.ITableItemProvider.GetColumnHeaderItems()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsArrayOfNativeAccessibles(this.publicITableItemProvider.GetColumnHeaderItems());
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00018190 File Offset: 0x00016390
		object UnsafeNativeMethods.IGridProvider.GetItem(int row, int column)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.AsNativeAccessible(this.publicIGridProvider.GetItem(row, column));
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x000181AF File Offset: 0x000163AF
		int UnsafeNativeMethods.IGridProvider.RowCount
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIGridProvider.RowCount;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x000181C6 File Offset: 0x000163C6
		int UnsafeNativeMethods.IGridProvider.ColumnCount
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIGridProvider.ColumnCount;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x000181DD File Offset: 0x000163DD
		int UnsafeNativeMethods.IGridItemProvider.Row
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIGridItemProvider.Row;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x000181F4 File Offset: 0x000163F4
		int UnsafeNativeMethods.IGridItemProvider.Column
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIGridItemProvider.Column;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x0001820B File Offset: 0x0001640B
		int UnsafeNativeMethods.IGridItemProvider.RowSpan
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIGridItemProvider.RowSpan;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00018222 File Offset: 0x00016422
		int UnsafeNativeMethods.IGridItemProvider.ColumnSpan
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicIGridItemProvider.ColumnSpan;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00018239 File Offset: 0x00016439
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.IGridItemProvider.ContainingGrid
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				if (AccessibilityImprovements.Level3)
				{
					return this.publicIGridItemProvider.ContainingGrid;
				}
				return this.AsNativeAccessible(this.publicIGridItemProvider.ContainingGrid) as UnsafeNativeMethods.IRawElementProviderSimple;
			}
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001826E File Offset: 0x0001646E
		object[] UnsafeNativeMethods.ISelectionProvider.GetSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicISelectionProvider.GetSelection();
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00018285 File Offset: 0x00016485
		bool UnsafeNativeMethods.ISelectionProvider.CanSelectMultiple
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicISelectionProvider.CanSelectMultiple;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0001829C File Offset: 0x0001649C
		bool UnsafeNativeMethods.ISelectionProvider.IsSelectionRequired
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicISelectionProvider.IsSelectionRequired;
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x000182B3 File Offset: 0x000164B3
		void UnsafeNativeMethods.ISelectionItemProvider.Select()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicISelectionItemProvider.Select();
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x000182CA File Offset: 0x000164CA
		void UnsafeNativeMethods.ISelectionItemProvider.AddToSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicISelectionItemProvider.AddToSelection();
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000182E1 File Offset: 0x000164E1
		void UnsafeNativeMethods.ISelectionItemProvider.RemoveFromSelection()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicISelectionItemProvider.RemoveFromSelection();
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x000182F8 File Offset: 0x000164F8
		bool UnsafeNativeMethods.ISelectionItemProvider.IsSelected
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicISelectionItemProvider.IsSelected;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0001830F File Offset: 0x0001650F
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.ISelectionItemProvider.SelectionContainer
		{
			get
			{
				IntSecurity.UnmanagedCode.Assert();
				return this.publicISelectionItemProvider.SelectionContainer;
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00018326 File Offset: 0x00016526
		void UnsafeNativeMethods.IScrollItemProvider.ScrollIntoView()
		{
			IntSecurity.UnmanagedCode.Assert();
			this.publicIScrollItemProvider.ScrollIntoView();
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001833D File Offset: 0x0001653D
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.IRawElementProviderHwndOverride.GetOverrideProviderForHwnd(IntPtr hwnd)
		{
			IntSecurity.UnmanagedCode.Assert();
			return this.publicIRawElementProviderHwndOverride.GetOverrideProviderForHwnd(hwnd);
		}

		// Token: 0x04000548 RID: 1352
		private IAccessible publicIAccessible;

		// Token: 0x04000549 RID: 1353
		private UnsafeNativeMethods.IEnumVariant publicIEnumVariant;

		// Token: 0x0400054A RID: 1354
		private UnsafeNativeMethods.IOleWindow publicIOleWindow;

		// Token: 0x0400054B RID: 1355
		private IReflect publicIReflect;

		// Token: 0x0400054C RID: 1356
		private UnsafeNativeMethods.IServiceProvider publicIServiceProvider;

		// Token: 0x0400054D RID: 1357
		private UnsafeNativeMethods.IAccessibleEx publicIAccessibleEx;

		// Token: 0x0400054E RID: 1358
		private UnsafeNativeMethods.IRawElementProviderSimple publicIRawElementProviderSimple;

		// Token: 0x0400054F RID: 1359
		private UnsafeNativeMethods.IRawElementProviderFragment publicIRawElementProviderFragment;

		// Token: 0x04000550 RID: 1360
		private UnsafeNativeMethods.IRawElementProviderFragmentRoot publicIRawElementProviderFragmentRoot;

		// Token: 0x04000551 RID: 1361
		private UnsafeNativeMethods.IInvokeProvider publicIInvokeProvider;

		// Token: 0x04000552 RID: 1362
		private UnsafeNativeMethods.IValueProvider publicIValueProvider;

		// Token: 0x04000553 RID: 1363
		private UnsafeNativeMethods.IRangeValueProvider publicIRangeValueProvider;

		// Token: 0x04000554 RID: 1364
		private UnsafeNativeMethods.IExpandCollapseProvider publicIExpandCollapseProvider;

		// Token: 0x04000555 RID: 1365
		private UnsafeNativeMethods.IToggleProvider publicIToggleProvider;

		// Token: 0x04000556 RID: 1366
		private UnsafeNativeMethods.ITableProvider publicITableProvider;

		// Token: 0x04000557 RID: 1367
		private UnsafeNativeMethods.ITableItemProvider publicITableItemProvider;

		// Token: 0x04000558 RID: 1368
		private UnsafeNativeMethods.IGridProvider publicIGridProvider;

		// Token: 0x04000559 RID: 1369
		private UnsafeNativeMethods.IGridItemProvider publicIGridItemProvider;

		// Token: 0x0400055A RID: 1370
		private UnsafeNativeMethods.ILegacyIAccessibleProvider publicILegacyIAccessibleProvider;

		// Token: 0x0400055B RID: 1371
		private UnsafeNativeMethods.ISelectionProvider publicISelectionProvider;

		// Token: 0x0400055C RID: 1372
		private UnsafeNativeMethods.ISelectionItemProvider publicISelectionItemProvider;

		// Token: 0x0400055D RID: 1373
		private UnsafeNativeMethods.IScrollItemProvider publicIScrollItemProvider;

		// Token: 0x0400055E RID: 1374
		private UnsafeNativeMethods.IRawElementProviderHwndOverride publicIRawElementProviderHwndOverride;

		// Token: 0x0400055F RID: 1375
		private UnsafeNativeMethods.UiaCore.ITextProvider2 publicITextProvider2;
	}
}
