using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Design.Behavior;
using Microsoft.Win32;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000355 RID: 853
	internal sealed class ToolStripDesignerUtils
	{
		// Token: 0x06002230 RID: 8752 RVA: 0x0000362F File Offset: 0x0000182F
		private ToolStripDesignerUtils()
		{
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000D18F0 File Offset: 0x000CFAF0
		public static void GetAdjustedBounds(ToolStripItem item, ref Rectangle r)
		{
			if (!(item is ToolStripControlHost) || !item.IsOnDropDown)
			{
				if (item is ToolStripMenuItem && item.IsOnDropDown)
				{
					r.Inflate(-3, -2);
					int width = r.Width;
					r.Width = width + 1;
					return;
				}
				if (item is ToolStripControlHost && !item.IsOnDropDown)
				{
					r.Inflate(0, -2);
					return;
				}
				if (item is ToolStripMenuItem && !item.IsOnDropDown)
				{
					r.Inflate(-3, -3);
					return;
				}
				r.Inflate(-1, -1);
			}
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x000D1978 File Offset: 0x000CFB78
		private static ToolStrip GetToolStripFromComponent(IComponent component)
		{
			ToolStripItem toolStripItem = component as ToolStripItem;
			ToolStrip result;
			if (toolStripItem != null)
			{
				if (!(toolStripItem is ToolStripDropDownItem))
				{
					result = toolStripItem.Owner;
				}
				else
				{
					result = ((ToolStripDropDownItem)toolStripItem).DropDown;
				}
			}
			else
			{
				result = (component as ToolStrip);
			}
			return result;
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000D19B8 File Offset: 0x000CFBB8
		private static ToolboxItem GetCachedToolboxItem(Type itemType)
		{
			ToolboxItem toolboxItem = null;
			if (ToolStripDesignerUtils.CachedToolboxItems == null)
			{
				ToolStripDesignerUtils.CachedToolboxItems = new Dictionary<Type, ToolboxItem>();
			}
			else if (ToolStripDesignerUtils.CachedToolboxItems.ContainsKey(itemType))
			{
				return ToolStripDesignerUtils.CachedToolboxItems[itemType];
			}
			if (toolboxItem == null)
			{
				toolboxItem = ToolboxService.GetToolboxItem(itemType);
				if (toolboxItem == null)
				{
					toolboxItem = new ToolboxItem(itemType);
				}
			}
			ToolStripDesignerUtils.CachedToolboxItems[itemType] = toolboxItem;
			if (ToolStripDesignerUtils.CustomToolStripItemCount > 0 && ToolStripDesignerUtils.CustomToolStripItemCount * 2 < ToolStripDesignerUtils.CachedToolboxItems.Count)
			{
				ToolStripDesignerUtils.CachedToolboxItems.Clear();
			}
			return toolboxItem;
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000D1A3C File Offset: 0x000CFC3C
		private static Bitmap GetKnownToolboxBitmap(Type itemType)
		{
			if (ToolStripDesignerUtils.CachedWinformsImages == null)
			{
				ToolStripDesignerUtils.CachedWinformsImages = new Dictionary<Type, Bitmap>();
			}
			if (!ToolStripDesignerUtils.CachedWinformsImages.ContainsKey(itemType))
			{
				Bitmap bitmap = ToolboxBitmapAttribute.GetImageFromResource(itemType, null, false) as Bitmap;
				ToolStripDesignerUtils.CachedWinformsImages[itemType] = bitmap;
				return bitmap;
			}
			return ToolStripDesignerUtils.CachedWinformsImages[itemType];
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x000D1A90 File Offset: 0x000CFC90
		public static Bitmap GetToolboxBitmap(Type itemType)
		{
			if (itemType.Namespace == ToolStripDesignerUtils.systemWindowsFormsNamespace)
			{
				return ToolStripDesignerUtils.GetKnownToolboxBitmap(itemType);
			}
			ToolboxItem cachedToolboxItem = ToolStripDesignerUtils.GetCachedToolboxItem(itemType);
			if (cachedToolboxItem != null)
			{
				return cachedToolboxItem.Bitmap;
			}
			return ToolStripDesignerUtils.GetKnownToolboxBitmap(typeof(Component));
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000D1AD8 File Offset: 0x000CFCD8
		public static string GetToolboxDescription(Type itemType)
		{
			string text = null;
			ToolboxItem cachedToolboxItem = ToolStripDesignerUtils.GetCachedToolboxItem(itemType);
			if (cachedToolboxItem != null)
			{
				text = cachedToolboxItem.DisplayName;
			}
			if (text == null)
			{
				text = itemType.Name;
			}
			if (text.StartsWith("ToolStrip"))
			{
				return text.Substring(9);
			}
			return text;
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x000D1B1C File Offset: 0x000CFD1C
		public static Type[] GetStandardItemTypes(IComponent component)
		{
			ToolStrip toolStripFromComponent = ToolStripDesignerUtils.GetToolStripFromComponent(component);
			if (toolStripFromComponent is MenuStrip)
			{
				return ToolStripDesignerUtils.NewItemTypesForMenuStrip;
			}
			if (toolStripFromComponent is ToolStripDropDownMenu)
			{
				return ToolStripDesignerUtils.NewItemTypesForToolStripDropDownMenu;
			}
			if (toolStripFromComponent is StatusStrip)
			{
				return ToolStripDesignerUtils.NewItemTypesForStatusStrip;
			}
			return ToolStripDesignerUtils.NewItemTypesForToolStrip;
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000D1B60 File Offset: 0x000CFD60
		private static ToolStripItemDesignerAvailability GetDesignerVisibility(ToolStrip toolStrip)
		{
			ToolStripItemDesignerAvailability result;
			if (toolStrip is StatusStrip)
			{
				result = ToolStripItemDesignerAvailability.StatusStrip;
			}
			else if (toolStrip is MenuStrip)
			{
				result = ToolStripItemDesignerAvailability.MenuStrip;
			}
			else if (toolStrip is ToolStripDropDownMenu)
			{
				result = ToolStripItemDesignerAvailability.ContextMenuStrip;
			}
			else
			{
				result = ToolStripItemDesignerAvailability.ToolStrip;
			}
			return result;
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000D1B98 File Offset: 0x000CFD98
		public static Type[] GetCustomItemTypes(IComponent component, IServiceProvider serviceProvider)
		{
			ITypeDiscoveryService discoveryService = null;
			if (serviceProvider != null)
			{
				discoveryService = (serviceProvider.GetService(typeof(ITypeDiscoveryService)) as ITypeDiscoveryService);
			}
			return ToolStripDesignerUtils.GetCustomItemTypes(component, discoveryService);
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000D1BC8 File Offset: 0x000CFDC8
		public static Type[] GetCustomItemTypes(IComponent component, ITypeDiscoveryService discoveryService)
		{
			if (discoveryService != null)
			{
				ICollection types = discoveryService.GetTypes(ToolStripDesignerUtils.toolStripItemType, false);
				ToolStrip toolStripFromComponent = ToolStripDesignerUtils.GetToolStripFromComponent(component);
				ToolStripItemDesignerAvailability designerVisibility = ToolStripDesignerUtils.GetDesignerVisibility(toolStripFromComponent);
				Type[] standardItemTypes = ToolStripDesignerUtils.GetStandardItemTypes(component);
				if (designerVisibility != ToolStripItemDesignerAvailability.None)
				{
					ArrayList arrayList = new ArrayList(types.Count);
					foreach (object obj in types)
					{
						Type type = (Type)obj;
						if (!type.IsAbstract && (type.IsPublic || type.IsNestedPublic) && !type.ContainsGenericParameters)
						{
							ConstructorInfo constructor = type.GetConstructor(new Type[0]);
							if (!(constructor == null))
							{
								ToolStripItemDesignerAvailabilityAttribute toolStripItemDesignerAvailabilityAttribute = (ToolStripItemDesignerAvailabilityAttribute)TypeDescriptor.GetAttributes(type)[typeof(ToolStripItemDesignerAvailabilityAttribute)];
								if (toolStripItemDesignerAvailabilityAttribute != null && (toolStripItemDesignerAvailabilityAttribute.ItemAdditionVisibility & designerVisibility) == designerVisibility)
								{
									bool flag = false;
									foreach (Type left in standardItemTypes)
									{
										if (left == type)
										{
											flag = true;
											break;
										}
									}
									if (!flag)
									{
										arrayList.Add(type);
									}
								}
							}
						}
					}
					if (arrayList.Count > 0)
					{
						Type[] array2 = new Type[arrayList.Count];
						arrayList.CopyTo(array2, 0);
						ToolStripDesignerUtils.CustomToolStripItemCount = arrayList.Count;
						return array2;
					}
				}
			}
			ToolStripDesignerUtils.CustomToolStripItemCount = 0;
			return new Type[0];
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000D1D4C File Offset: 0x000CFF4C
		public static ToolStripItem[] GetStandardItemMenuItems(IComponent component, EventHandler onClick, bool convertTo)
		{
			Type[] standardItemTypes = ToolStripDesignerUtils.GetStandardItemTypes(component);
			ToolStripItem[] array = new ToolStripItem[standardItemTypes.Length];
			for (int i = 0; i < standardItemTypes.Length; i++)
			{
				ItemTypeToolStripMenuItem itemTypeToolStripMenuItem = new ItemTypeToolStripMenuItem(standardItemTypes[i]);
				itemTypeToolStripMenuItem.ConvertTo = convertTo;
				if (onClick != null)
				{
					itemTypeToolStripMenuItem.Click += onClick;
				}
				array[i] = itemTypeToolStripMenuItem;
			}
			return array;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000D1D98 File Offset: 0x000CFF98
		public static ToolStripItem[] GetCustomItemMenuItems(IComponent component, EventHandler onClick, bool convertTo, IServiceProvider serviceProvider)
		{
			Type[] customItemTypes = ToolStripDesignerUtils.GetCustomItemTypes(component, serviceProvider);
			ToolStripItem[] array = new ToolStripItem[customItemTypes.Length];
			for (int i = 0; i < customItemTypes.Length; i++)
			{
				ItemTypeToolStripMenuItem itemTypeToolStripMenuItem = new ItemTypeToolStripMenuItem(customItemTypes[i]);
				itemTypeToolStripMenuItem.ConvertTo = convertTo;
				if (onClick != null)
				{
					itemTypeToolStripMenuItem.Click += onClick;
				}
				array[i] = itemTypeToolStripMenuItem;
			}
			return array;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000D1DE4 File Offset: 0x000CFFE4
		public static NewItemsContextMenuStrip GetNewItemDropDown(IComponent component, ToolStripItem currentItem, EventHandler onClick, bool convertTo, IServiceProvider serviceProvider, bool populateCustom)
		{
			NewItemsContextMenuStrip newItemsContextMenuStrip = new NewItemsContextMenuStrip(component, currentItem, onClick, convertTo, serviceProvider);
			newItemsContextMenuStrip.GroupOrdering.Add("StandardList");
			newItemsContextMenuStrip.GroupOrdering.Add("CustomList");
			foreach (ToolStripItem toolStripItem in ToolStripDesignerUtils.GetStandardItemMenuItems(component, onClick, convertTo))
			{
				newItemsContextMenuStrip.Groups["StandardList"].Items.Add(toolStripItem);
				if (convertTo)
				{
					ItemTypeToolStripMenuItem itemTypeToolStripMenuItem = toolStripItem as ItemTypeToolStripMenuItem;
					if (itemTypeToolStripMenuItem != null && currentItem != null && itemTypeToolStripMenuItem.ItemType == currentItem.GetType())
					{
						itemTypeToolStripMenuItem.Enabled = false;
					}
				}
			}
			if (populateCustom)
			{
				ToolStripDesignerUtils.GetCustomNewItemDropDown(newItemsContextMenuStrip, component, currentItem, onClick, convertTo, serviceProvider);
			}
			IUIService iuiservice = serviceProvider.GetService(typeof(IUIService)) as IUIService;
			if (iuiservice != null)
			{
				newItemsContextMenuStrip.Renderer = (ToolStripProfessionalRenderer)iuiservice.Styles["VsRenderer"];
				newItemsContextMenuStrip.Font = (Font)iuiservice.Styles["DialogFont"];
				if (iuiservice.Styles["VsColorPanelText"] is Color)
				{
					newItemsContextMenuStrip.ForeColor = (Color)iuiservice.Styles["VsColorPanelText"];
				}
			}
			newItemsContextMenuStrip.Populate();
			return newItemsContextMenuStrip;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000D1F20 File Offset: 0x000D0120
		public static void GetCustomNewItemDropDown(NewItemsContextMenuStrip contextMenu, IComponent component, ToolStripItem currentItem, EventHandler onClick, bool convertTo, IServiceProvider serviceProvider)
		{
			foreach (ToolStripItem toolStripItem in ToolStripDesignerUtils.GetCustomItemMenuItems(component, onClick, convertTo, serviceProvider))
			{
				contextMenu.Groups["CustomList"].Items.Add(toolStripItem);
				if (convertTo)
				{
					ItemTypeToolStripMenuItem itemTypeToolStripMenuItem = toolStripItem as ItemTypeToolStripMenuItem;
					if (itemTypeToolStripMenuItem != null && currentItem != null && itemTypeToolStripMenuItem.ItemType == currentItem.GetType())
					{
						itemTypeToolStripMenuItem.Enabled = false;
					}
				}
			}
			contextMenu.Populate();
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000D1F98 File Offset: 0x000D0198
		public static void InvalidateSelection(ArrayList originalSelComps, ToolStripItem nextSelection, IServiceProvider provider, bool shiftPressed)
		{
			if (nextSelection == null || provider == null)
			{
				return;
			}
			Region region = null;
			Region region2 = null;
			int num = 1;
			int num2 = 2;
			bool flag = false;
			try
			{
				Rectangle rect = Rectangle.Empty;
				IDesignerHost designerHost = (IDesignerHost)provider.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					foreach (object obj in originalSelComps)
					{
						Component component = (Component)obj;
						ToolStripItem toolStripItem = component as ToolStripItem;
						if (toolStripItem != null && (originalSelComps.Count > 1 || (originalSelComps.Count == 1 && toolStripItem.GetCurrentParent() != nextSelection.GetCurrentParent()) || toolStripItem is ToolStripSeparator || toolStripItem is ToolStripControlHost || !toolStripItem.IsOnDropDown || toolStripItem.IsOnOverflow))
						{
							ToolStripItemDesigner toolStripItemDesigner = designerHost.GetDesigner(toolStripItem) as ToolStripItemDesigner;
							if (toolStripItemDesigner != null)
							{
								rect = toolStripItemDesigner.GetGlyphBounds();
								ToolStripDesignerUtils.GetAdjustedBounds(toolStripItem, ref rect);
								rect.Inflate(num, num);
								if (region == null)
								{
									region = new Region(rect);
									rect.Inflate(-num2, -num2);
									region.Exclude(rect);
								}
								else
								{
									region2 = new Region(rect);
									rect.Inflate(-num2, -num2);
									region2.Exclude(rect);
									region.Union(region2);
								}
							}
							else if (toolStripItem is DesignerToolStripControlHost)
							{
								flag = true;
							}
						}
					}
				}
				if (region != null || flag || shiftPressed)
				{
					BehaviorService behaviorService = (BehaviorService)provider.GetService(typeof(BehaviorService));
					if (behaviorService != null)
					{
						if (region != null)
						{
							behaviorService.Invalidate(region);
						}
						ToolStripItemDesigner toolStripItemDesigner = designerHost.GetDesigner(nextSelection) as ToolStripItemDesigner;
						if (toolStripItemDesigner != null)
						{
							rect = toolStripItemDesigner.GetGlyphBounds();
							ToolStripDesignerUtils.GetAdjustedBounds(nextSelection, ref rect);
							rect.Inflate(num, num);
							region = new Region(rect);
							rect.Inflate(-num2, -num2);
							region.Exclude(rect);
							behaviorService.Invalidate(region);
						}
					}
				}
			}
			finally
			{
				if (region != null)
				{
					region.Dispose();
				}
				if (region2 != null)
				{
					region2.Dispose();
				}
			}
		}

		// Token: 0x0400198D RID: 6541
		private static Type toolStripItemType = typeof(ToolStripItem);

		// Token: 0x0400198E RID: 6542
		[ThreadStatic]
		private static Dictionary<Type, ToolboxItem> CachedToolboxItems;

		// Token: 0x0400198F RID: 6543
		[ThreadStatic]
		private static int CustomToolStripItemCount = 0;

		// Token: 0x04001990 RID: 6544
		private const int TOOLSTRIPCHARCOUNT = 9;

		// Token: 0x04001991 RID: 6545
		public static ArrayList originalSelComps;

		// Token: 0x04001992 RID: 6546
		[ThreadStatic]
		private static Dictionary<Type, Bitmap> CachedWinformsImages;

		// Token: 0x04001993 RID: 6547
		private static string systemWindowsFormsNamespace = typeof(ToolStripItem).Namespace;

		// Token: 0x04001994 RID: 6548
		private static readonly Type[] NewItemTypesForToolStrip = new Type[]
		{
			typeof(ToolStripButton),
			typeof(ToolStripLabel),
			typeof(ToolStripSplitButton),
			typeof(ToolStripDropDownButton),
			typeof(ToolStripSeparator),
			typeof(ToolStripComboBox),
			typeof(ToolStripTextBox),
			typeof(ToolStripProgressBar)
		};

		// Token: 0x04001995 RID: 6549
		private static readonly Type[] NewItemTypesForStatusStrip = new Type[]
		{
			typeof(ToolStripStatusLabel),
			typeof(ToolStripProgressBar),
			typeof(ToolStripDropDownButton),
			typeof(ToolStripSplitButton)
		};

		// Token: 0x04001996 RID: 6550
		private static readonly Type[] NewItemTypesForMenuStrip = new Type[]
		{
			typeof(ToolStripMenuItem),
			typeof(ToolStripComboBox),
			typeof(ToolStripTextBox)
		};

		// Token: 0x04001997 RID: 6551
		private static readonly Type[] NewItemTypesForToolStripDropDownMenu = new Type[]
		{
			typeof(ToolStripMenuItem),
			typeof(ToolStripComboBox),
			typeof(ToolStripSeparator),
			typeof(ToolStripTextBox)
		};

		// Token: 0x02000598 RID: 1432
		internal static class DisplayInformation
		{
			// Token: 0x06003344 RID: 13124 RVA: 0x001182E6 File Offset: 0x001164E6
			static DisplayInformation()
			{
				SystemEvents.UserPreferenceChanged += ToolStripDesignerUtils.DisplayInformation.UserPreferenceChanged;
				SystemEvents.DisplaySettingsChanged += ToolStripDesignerUtils.DisplayInformation.DisplaySettingChanged;
			}

			// Token: 0x17000A02 RID: 2562
			// (get) Token: 0x06003345 RID: 13125 RVA: 0x0011830C File Offset: 0x0011650C
			public static short BitsPerPixel
			{
				get
				{
					if (ToolStripDesignerUtils.DisplayInformation.bitsPerPixel == 0)
					{
						new EnvironmentPermission(PermissionState.Unrestricted).Assert();
						try
						{
							foreach (Screen screen in Screen.AllScreens)
							{
								if (ToolStripDesignerUtils.DisplayInformation.bitsPerPixel == 0)
								{
									ToolStripDesignerUtils.DisplayInformation.bitsPerPixel = (short)screen.BitsPerPixel;
								}
								else
								{
									ToolStripDesignerUtils.DisplayInformation.bitsPerPixel = (short)Math.Min(screen.BitsPerPixel, (int)ToolStripDesignerUtils.DisplayInformation.bitsPerPixel);
								}
							}
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					return ToolStripDesignerUtils.DisplayInformation.bitsPerPixel;
				}
			}

			// Token: 0x17000A03 RID: 2563
			// (get) Token: 0x06003346 RID: 13126 RVA: 0x0011838C File Offset: 0x0011658C
			public static bool LowResolution
			{
				get
				{
					if (ToolStripDesignerUtils.DisplayInformation.lowResSettingValid)
					{
						return ToolStripDesignerUtils.DisplayInformation.lowRes;
					}
					ToolStripDesignerUtils.DisplayInformation.lowRes = (ToolStripDesignerUtils.DisplayInformation.BitsPerPixel <= 8);
					ToolStripDesignerUtils.DisplayInformation.lowResSettingValid = true;
					return ToolStripDesignerUtils.DisplayInformation.lowRes;
				}
			}

			// Token: 0x17000A04 RID: 2564
			// (get) Token: 0x06003347 RID: 13127 RVA: 0x001183B6 File Offset: 0x001165B6
			public static bool HighContrast
			{
				get
				{
					if (ToolStripDesignerUtils.DisplayInformation.highContrastSettingValid)
					{
						return ToolStripDesignerUtils.DisplayInformation.highContrast;
					}
					ToolStripDesignerUtils.DisplayInformation.highContrast = SystemInformation.HighContrast;
					ToolStripDesignerUtils.DisplayInformation.highContrastSettingValid = true;
					return ToolStripDesignerUtils.DisplayInformation.highContrast;
				}
			}

			// Token: 0x17000A05 RID: 2565
			// (get) Token: 0x06003348 RID: 13128 RVA: 0x001183DA File Offset: 0x001165DA
			public static bool IsDropShadowEnabled
			{
				get
				{
					if (ToolStripDesignerUtils.DisplayInformation.dropShadowSettingValid)
					{
						return ToolStripDesignerUtils.DisplayInformation.dropShadowEnabled;
					}
					ToolStripDesignerUtils.DisplayInformation.dropShadowEnabled = SystemInformation.IsDropShadowEnabled;
					ToolStripDesignerUtils.DisplayInformation.dropShadowSettingValid = true;
					return ToolStripDesignerUtils.DisplayInformation.dropShadowEnabled;
				}
			}

			// Token: 0x17000A06 RID: 2566
			// (get) Token: 0x06003349 RID: 13129 RVA: 0x001183FE File Offset: 0x001165FE
			public static bool TerminalServer
			{
				get
				{
					if (ToolStripDesignerUtils.DisplayInformation.terminalSettingValid)
					{
						return ToolStripDesignerUtils.DisplayInformation.isTerminalServerSession;
					}
					ToolStripDesignerUtils.DisplayInformation.isTerminalServerSession = SystemInformation.TerminalServerSession;
					ToolStripDesignerUtils.DisplayInformation.terminalSettingValid = true;
					return ToolStripDesignerUtils.DisplayInformation.isTerminalServerSession;
				}
			}

			// Token: 0x0600334A RID: 13130 RVA: 0x00118422 File Offset: 0x00116622
			private static void DisplaySettingChanged(object obj, EventArgs ea)
			{
				ToolStripDesignerUtils.DisplayInformation.highContrastSettingValid = false;
				ToolStripDesignerUtils.DisplayInformation.lowResSettingValid = false;
				ToolStripDesignerUtils.DisplayInformation.terminalSettingValid = false;
				ToolStripDesignerUtils.DisplayInformation.dropShadowSettingValid = false;
			}

			// Token: 0x0600334B RID: 13131 RVA: 0x0011843C File Offset: 0x0011663C
			private static void UserPreferenceChanged(object obj, UserPreferenceChangedEventArgs ea)
			{
				ToolStripDesignerUtils.DisplayInformation.highContrastSettingValid = false;
				ToolStripDesignerUtils.DisplayInformation.lowResSettingValid = false;
				ToolStripDesignerUtils.DisplayInformation.terminalSettingValid = false;
				ToolStripDesignerUtils.DisplayInformation.dropShadowSettingValid = false;
				ToolStripDesignerUtils.DisplayInformation.bitsPerPixel = 0;
			}

			// Token: 0x04002252 RID: 8786
			private static bool highContrast;

			// Token: 0x04002253 RID: 8787
			private static bool lowRes;

			// Token: 0x04002254 RID: 8788
			private static bool isTerminalServerSession;

			// Token: 0x04002255 RID: 8789
			private static bool highContrastSettingValid;

			// Token: 0x04002256 RID: 8790
			private static bool lowResSettingValid;

			// Token: 0x04002257 RID: 8791
			private static bool terminalSettingValid;

			// Token: 0x04002258 RID: 8792
			private static short bitsPerPixel;

			// Token: 0x04002259 RID: 8793
			private static bool dropShadowSettingValid;

			// Token: 0x0400225A RID: 8794
			private static bool dropShadowEnabled;
		}
	}
}
