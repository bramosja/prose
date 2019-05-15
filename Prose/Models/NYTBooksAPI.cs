
using System;
using System.Collections.Generic;

namespace Prose.Models
{
    public class NYTBooksAPI
    {
        public string status { get; set; }
        public string copyright { get; set; }
        public int num_results { get; set; }
        public DateTime last_modified { get; set; }
        public Results results { get; set; }
    }

    public class Results
    {
        public string list_name { get; set; }
        public string list_name_encoded { get; set; }
        public string bestsellers_date { get; set; }
        public string published_date { get; set; }
        public string published_date_description { get; set; }
        public string next_published_date { get; set; }
        public string previous_published_date { get; set; }
        public string display_name { get; set; }
        public int normal_list_ends_at { get; set; }
        public string updated { get; set; }
        public List<Booki> books { get; set; }
        public List<object> corrections { get; set; }
    }

    public class Booki
    {
        public int rank { get; set; }
        public int rank_last_week { get; set; }
        public int weeks_on_list { get; set; }
        public int asterisk { get; set; }
        public int dagger { get; set; }
        public string primary_isbn10 { get; set; }
        public string primary_isbn13 { get; set; }
        public string publisher { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string contributor { get; set; }
        public string contributor_note { get; set; }
        public string book_image { get; set; }
        public int book_image_width { get; set; }
        public int book_image_height { get; set; }
        public string amazon_product_url { get; set; }
        public string age_group { get; set; }
        public string book_review_link { get; set; }
        public string first_chapter_link { get; set; }
        public string sunday_review_link { get; set; }
        public string article_chapter_link { get; set; }
        public List<Isbn> isbns { get; set; }
        public List<Buy_Links> buy_links { get; set; }
    }

    public class Isbn
    {
        public string isbn10 { get; set; }
        public string isbn13 { get; set; }
    }

    public class Buy_Links
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}

