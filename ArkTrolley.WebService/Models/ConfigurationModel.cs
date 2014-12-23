using System;
using System.Collections.Generic;

namespace ArkTrolley.WebService
{
	public class ConfigurationData
	{
		public string int_id { get; set; }
		public string int_active { get; set; }
		public string int_device_type_smarthphone_hits { get; set; }
		public string int_device_type_tablet_hits { get; set; }
		public string int_device_type_others_hits { get; set; }
		public string enm_device_size { get; set; }
		public string dte_register { get; set; }
		public string str_covered_models { get; set; }
		public string str_web_service_version { get; set; }
		public string str_web_service_root { get; set; }
		public string str_assets_path { get; set; }
		public string str_store_chains_pictures_path { get; set; }
		public string str_items_pictures_path { get; set; }
		public string str_stores_pictures_path { get; set; }
		public string str_users_pictures_path { get; set; }
		public string str_recipes_pictures_path { get; set; }
		public string str_another_user_button_title { get; set; }
		public string str_another_user_button_sub_line { get; set; }
		public string str_create_user_button_title { get; set; }
		public string str_register_store_button_title { get; set; }
		public string str_remember_me_checkbox_title { get; set; }
		public string str_cant_login_link_title { get; set; }
		public string str_no_store_selected_title { get; set; }
		public string str_my_profile_button_title { get; set; }
		public string str_my_lists_button_title { get; set; }
		public string str_my_tickets_button_title { get; set; }
		public string str_my_recipes_button_title { get; set; }
		public string str_search_item_button_title { get; set; }
		public string str_search_item_title_picture { get; set; }
		public string str_current_trolley_cost_text { get; set; }
		public string str_items_comparison_note_text { get; set; }
		public string str_my_lists_text { get; set; }
		public string str_selected_list_text { get; set; }
		public string str_my_tickets_text { get; set; }
		public string str_selected_ticket_note { get; set; }
		public string str_selected_ticket_text { get; set; }
		public string str_my_recipes_text { get; set; }
		public string str_selected_recipe_text { get; set; }
		public string str_finish_current_trolley_text { get; set; }
		public string str_finish_current_trolley_question { get; set; }
		public string str_header_picture { get; set; }
		public string str_footer_picture { get; set; }
		public string str_another_user_button_picture { get; set; }
		public string str_create_user_button_picture { get; set; }
		public string str_register_store_button_picture { get; set; }
		public string str_my_profile_button_picture { get; set; }
		public string str_my_recipes_button_picture { get; set; }
		public string str_my_lists_button_picture { get; set; }
		public string str_my_tickets_button_picture { get; set; }
		public string str_search_item_button_picture { get; set; }
		public string str_settings_button_picture { get; set; }
		public string str_current_trolley_button_picture { get; set; }
		public string str_barcode_scanner_button_picture { get; set; }
		public string str_pick_store_title_picture { get; set; }
		public string str_current_trolley_title_picture { get; set; }
		public string str_items_comparison_title_picture { get; set; }
		public string str_my_lists_title_picture { get; set; }
		public string str_my_recipes_title_picture { get; set; }
		public string str_my_tickets_title_picture { get; set; }
		public string str_selected_list_title_picture { get; set; }
		public string str_selected_recipe_title_picture { get; set; }
		public string str_selected_ticket_title_picture { get; set; }
		public string str_user_profile_title_picture { get; set; }
		public string str_regular_price_icon_picture { get; set; }
		public string str_special_price_icon_picture { get; set; }
		public string str_coupon_price_icon_picture { get; set; }
		public string str_clearance_price_icon_picture { get; set; }
		public string str_free_price_icon_picture { get; set; }
		public string str_remove_icon_picture { get; set; }
		public string str_left_arrow_icon_picture { get; set; }
		public string str_right_arrow_icon_picture { get; set; }
		public string str_up_arrow_icon_picture { get; set; }
		public string str_down_arrow_icon_picture { get; set; }
		public string str_login_button_picture { get; set; }
		public string str_cancel_button_picture { get; set; }
		public string str_refresh_button_picture { get; set; }
		public string str_update_store_button_picture { get; set; }
		public string str_logout_button_picture { get; set; }
		public string str_finish_button_picture { get; set; }
		public string str_close_button_picture { get; set; }
		public string str_add_button_picture { get; set; }
		public string str_save_recipe_button_picture { get; set; }
		public string str_browse_picture_button_picture { get; set; }
		public string str_search_button_picture { get; set; }
		public string str_finish_current_trolley_paid_button_picture { get; set; }
		public string str_finish_current_trolley_canceled_button_picture { get; set; }
		public string str_save_user_button_picture { get; set; }
	}

	public class ConfigurationModel
	{
		public int responseCode { get; set; }
		public string responseMessage { get; set; }
		public ConfigurationData responseData { get; set; }
	}
}

