using System;
using ArkTrolley.WebService;
using ArkTrolley.Core.AppHelper;

namespace ArkTrolley.Core
{
	public class ConfigurationSettings
	{
		public ConfigurationSettings ()
		{
		}

		public static string GetHeaderImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
				SharedData.CurrentConfigurationData.str_assets_path +
				SharedData.CurrentConfigurationData.str_header_picture;
			}
		}

		public static string GetFooterImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_footer_picture;
			}
		}

		public static string GetAnotherUserButtonImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_another_user_button_picture;
			}
		}

		public static string GetCreateUserButtonImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_create_user_button_picture;
			}
		}

		public static string GetRegisterUserButtonImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_register_store_button_picture;
			}
		}

		public static string GetMyProfileButtonImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_my_profile_button_picture;
			}
		}

		public static string GetMyRecipesButtonImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_my_recipes_button_picture;
			}
		}

		public static string GetMyListsButtonImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_my_lists_button_picture;
			}
		}

		public static string GetMyTicketsButtonImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_my_tickets_button_picture;
			}
		}

		public static string GetSearchItemButtonImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
				SharedData.CurrentConfigurationData.str_assets_path +
				SharedData.CurrentConfigurationData.str_search_item_button_picture;
			}
		}

		public static string GetSettingsButtonImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_settings_button_picture;
			}
		}

		public static string GetCurrentTrolleyButtonImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_current_trolley_button_picture;
			}
		}

		public static string GetBarcodeScannerButtonImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_barcode_scanner_button_picture;
			}
		}

		public static string GetPickStoreTitleImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_pick_store_title_picture;
			}
		}

		public static string GetCurrentTrolleyTitleImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_current_trolley_title_picture;
			}
		}

		public static string GetItemsComparisonTitleImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_items_comparison_title_picture;
			}
		}

		public static string GetMyListTitleImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_my_lists_title_picture;
			}
		}

		public static string GetMyRecipesTitleImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_my_recipes_title_picture;
			}
		}

		public static string GetMyTicketsTitleImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_my_tickets_title_picture;
			}
		}

		public static string GetSelectedListTitleImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_selected_list_title_picture;
			}
		}

		public static string GetSelectedRecipeTitleImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_selected_recipe_title_picture;
			}
		}

		public static string GetSelectedTicketTitleImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
					SharedData.CurrentConfigurationData.str_assets_path +
					SharedData.CurrentConfigurationData.str_selected_ticket_title_picture;
			}
		}

		public static string GetSearchItemTitleImagePath {
			get {
				return SharedData.CurrentConfigurationData.str_web_service_root +
				SharedData.CurrentConfigurationData.str_assets_path +
				SharedData.CurrentConfigurationData.str_search_item_title_picture;
			}
		}
	}
}

