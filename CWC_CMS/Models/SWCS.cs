using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIDCUL.Areas.Services.Models
{
    public class SWCS
    {
        public int STATUS { get; set; }
        public string MSG { get; set; }
        public RESPONSE RESPONSE { get; set; }
    }

    public class RESPONSE
    {
        public string unit_name { get; set; }
        public string app_name { get; set; }
        public string app_status { get; set; }
        public string app_comments { get; set; }
        public string app_distt { get; set; }
        public string app_location { get; set; }
        public string app_caf_id { get; set; }
        public string created_on { get; set; }
        public string CAFFields { get; set; }
        public string token_created_on { get; set; }
        public string token { get; set; }
        public object token_access_on { get; set; }
        public string email { get; set; }
        public string iuid { get; set; }
        public string profile_id { get; set; }
        public string user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string pan_card { get; set; }
        public string adhaar_number { get; set; }
        public string country_name { get; set; }
        public string state_name { get; set; }
        public string city_name { get; set; }
        public string distt_name { get; set; }
        public string pin_code { get; set; }
        public string address { get; set; }
        public string mobile_number { get; set; }
        public string auth_email { get; set; }
        public string company_name { get; set; }
        public List<Document> documents { get; set; }
        public PaymentInfo payment_info { get; set; }
    }
    public class Document
    {
        public string code { get; set; }
        public string document_name { get; set; }
        public string file_name { get; set; }
        public string document_status { get; set; }
    }
    public class PaymentInfo
    {
        public string payment_type { get; set; }
        public string payment_mode { get; set; }
        public string payment_datetime { get; set; }
        public string paid_amount { get; set; }
        public string reference_number { get; set; }
        public string treasury_head_detail { get; set; }
        public string recipent_bank_name { get; set; }
        public string recipent_bank_ac_no { get; set; }
        public string recipent_bank_ifsc_code { get; set; }
    }

    public class DepartmentalSingleSignOn
    { 
        public int department_id { get; set; }
        public string officer_key { get; set; }
        public string sp_tag { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string mobile_number { get; set; }
        public string role { get; set; }
        public int district_id { get; set; }
        public int dept_application_id { get; set; }
        public int CALL_BACK_URL { get; set; }
    }
}