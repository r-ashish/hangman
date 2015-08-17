using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace App5
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        public static string sLose()
        {
            if (wrong == 6 || x1 == "Yes") return quest;
            return "";
        }
        public static int[] vis = new int[26];
        private void showButtons()
        {
            int upMargin = 140;
            int leftMargin = -285;
            for ( int i = 0; i < 26; i++ )
            {
                Button b = new Button();
                b.Height = 60;
                b.Width = 40;
                b.MinHeight = 0;
                b.MinWidth = 0;
                b.Visibility = Windows.UI.Xaml.Visibility.Visible;
                if(vis[i] == 1) b.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                b.FontFamily = new FontFamily("/Fonts/Segoe.ttf#Segoe Print");
                b.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                b.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom;
                b.Content = alphabets[i];
                b.Name = alphabets[i].ToString();
                b.Click += a_Click;
                Thickness margin = b.Margin;
                margin.Left = leftMargin;
                margin.Bottom = upMargin;
                b.Margin = margin;
                mainGrid.Children.Add(b);
                leftMargin += 85;
                if((i+1)%8 == 0)
                {
                    upMargin -= 46;
                    leftMargin = -285;
                }
            }
        }
        string handleSpace(string a,string b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if(a[i]==' ')
                {
                    b = b.Remove(2 * i, 1);
                    b = b.Insert(2 * i," ");
                }
            }
            return b;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter!=null) 
            {
                prevScore = (int)e.Parameter;
                score = prevScore;
                Random ran = new Random();
                category = categories[selectedInd];
                array = category.Split();
                ind = ran.Next(0,array.Length);
                quest = array[ind].Replace("-"," ").ToLower();
                temp = handleSpace(quest,String.Concat(Enumerable.Repeat("_ ", quest.Length)));
                already = "";
                correct = 0;
                if (quest.Contains(" "))
                {
                    int count = 0;
                    for (int i = 0; i < quest.Length; i++)
                    {
                        if (quest[i] == ' ')
                        {
                            count += 1;
                        }
                    }
                    correct += count;
                }
                wrong = 0;
                vis = new int[26];
            }
            msgPopup.ChildTransitions = new TransitionCollection { new PaneThemeTransition { Edge = EdgeTransitionLocation.Left } };
            showButtons();
            int x = 6 - wrong;
            chances.Text = "Chances Left : "+ x;
            scoreBlock.Text = "Score : " + score;
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += Hardwarebuttons_BackPressed;
            Block.Text = display()[wrong];
            question.Text = temp;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= Hardwarebuttons_BackPressed;
        }
        private void Hardwarebuttons_BackPressed(object sender ,Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (rootFra == null) return;
            else
            {
                 rootFra.Navigate(typeof(HomePage),"a");
                 e.Handled = true;
            }
        }
        static int[] check(char s, string quest)
        {
            if (quest.IndexOf(s) == -1)
            {
                int[] a = { -1 };
                return a;
            }
            else
            {
                int count = 0;
                for (int i = 0; i < quest.Length; i++)
                {
                    if (quest[i] == s)
                    {
                        count += 1;
                    }
                }
                int[] a = new int[count];
                int c = 0;
                for (int i = 0; i < quest.Length; i++)
                {

                    if (quest[i] == s)
                    {
                        a[c] = i;
                        c += 1;
                    }
                }
                return a;
            }
        }
        static bool checkL(char c, char[] cA)
        {
            for (int i = 0; i < cA.Length; i++)
            {
                if (cA[i] == c) return true;
            }
            return false;
        }
        public static string[] display()
        {
            string display1 = "    " + "+----+\n " + "           |\n " + "           |\n " + "           |\n " + "           |\n " + "           |\n " + "  =======";
            string display2 = "     " + "+----+\n " + "     O     |\n " + "            |\n " + "            |\n " + "            |\n " + "            |\n " + "  =======";
            string display3 = "     " + "+----+\n " + "     O     |\n " + "     |      |\n " + "            |\n " + "            |\n " + "            |\n " + "  =======";
            string display4 = "     " + "+----+\n " + "     O     |\n " + "    /|      |\n " + "            |\n " + "            |\n " + "            |\n " + "  =======";
            string display5 = "     " + "+----+\n " + "     O     |\n " + "    /|\\    |\n " + "            |\n " + "            |\n " + "            |\n " + "  =======";
            string display6 = "     " + "+----+\n " + "     O     |\n " + "    /|\\    |\n " +
                                "    /       |\n " + "            |\n " + "            |\n " + "   =======";
            string display7 = "     " + "+----+\n " + "     O     |\n " + "    /|\\    |\n " +
                                "    / \\    |\n " + "            |\n " + "            |\n " + "   =======";
            string[] r = { display1, display2, display3, display4, display5, display6, display7 };
            return r;
        }
        public static int score;
        public static int selectedInd;
        public static int prevScore;
        public static string[] categories = {
                                                "cat wolf penguin elephant buffalo Abyssinian Affenpinscher Ainu-Dog Akbash Akita Malamute Albatross Alligator Spaniel Foxhound Ant Anteater Antelope Arctic-Hare Arctic-Wolf Armadillo Giant-Hornet Palm-Civet Avocet Axolotl Aye-Aye Baboon Camel Badger Balinese Bandicoot Barb Barn-Owl Barnacle Barracuda Basenji-Dog Basking-Shark Basset-Hound Bat Beagle Bear Bearded-Collie Bearded-Dragon Beaver Beetle Bengal-Tiger Bichon-Frise Binturong Bird Birman Bison Black-BearBloodhound Lacy-Dog Blue-Whale Coonhound Bobcat Bombay Bongo Bonobo Booby Boxer-Dog Brown-Bear Budgerigar Buffalo Bull-Mastiff Bull-Shark Bulldog Bullfrog Bumble-Bee Burmese Butterfly Caiman Cairn-Terrier Camel Capybara Caracal Cassowary Cat Caterpillar Catfish Cavalier-King-Charles-Spaniel Centipede Cesky-Fousek Chameleon Chamois Cheetah Chicken Chihuahua Chimpanzee Chinchilla Chinook Chipmunk Cichlid Clown-Fish Coati Cockroach Collared-Peccary Collie Buzzard Loon Coral Cougar Cow Coyote Crab Crane Crocodile Cuscus Cuttlefish Dachshund Dalmatian Deer Dhole Dingo Discus Dodo Dog Dolphin Donkey Dormouse Dragonfly Drever Duck Dugong Dunker Dolphin Crocodile Eagle Earwig Echidna Edible-Frog Egyptian-Mau Electric-Eel Elephant Elephant-Seal Elephant-Shrew Emu Eskimo-Dog Falcon Fennec-Fox Ferret Field-Spaniel Fin-Whale Finnish-Spitz Fishing-Cat Flamingo Flounder Fossa Fox Fox-Terrier French-Bulldog Frigatebird Frog Fur-Seal Gar Geck Gerbil Pinscher Gharial Land-Snail Giant-Clam Giant-Panda-Bear Giant-Schnauzer Gibbon Gila-Monster Giraffe Glass-Lizard Glow-Worm Goat Golden-Oriole Goose Gopher Gorilla Grasshopper Great-Dane White-Shark Mountain-Dog Grey-Seal Greyhound Grizzly-Bear Grouse Guinea-Fowl Guinea-Pig Guppy Hamster Hare Harrier Havanese Hedgehog Hermit-Crab Heron Himalayan Hippopotamus Honey-Bee Horn-Shark Horned-Frog Horse Hummingbird Hyena Ibis Hound Iguana Impala Rhinoceros Tortoise Setter WolfHound Jack-Russel Jackal Jaguar Chin Macaque Rhinoceros Javanese Jellyfish Kakapo Kangaroo Toucan Whale King-Crab King-Penguin Kingfisher Kiwi Koala Kudu Labradoodle Ladybird Gecko Lemming Lemur Leopard Leopard-Cat Leopard-Seal Liger Lion Lionfish Lizard Llama Lobster Lynx Macaw Magpie Maine-Coon Malayan-Civet Malayan-Tiger Maltese Manatee Mandrill Manta-Ray Marine-Toad Markhor Marsh-Frog Mastiff Mayfly Meerkat Millipede Minke-Whale Mole Molly Mongoose Mongrel Monitor-Lizard Monkey Moorhen Moose Moray-Eel Moth Mountain-Lion Mouse Mule Neanderthal Newfoundland Newt Nightingale Norfolk-Terrier Forest Numbat Nurse-Shark Ocelot Octopus Okapi Olm Opossum Orang-utan Ostrich Otter Oyster Pademelon Panther Parrot Peacock Pekingese Pelican Penguin Persian Pheasant Pied-Tamarin Pig Pika Pike Armadillo Piranha Platypus Pointer Polar-Bear Pond-Skater Poodle Pool-Frog Porcupine Possum Prawn Puffer-Fish Puffin Pug Puma Puss-Moth Hippopotamus Pygmy-Marmoset Quail Quetzal Quokka Quoll Rabbit Raccoon Radiated-Tortoise Ragdoll Rat Rattlesnake Tarantula Red-Panda Red-Wolf Tamarin Reindeer Rhinoceros River-Dolphin River-Turtle Robin Rock-Hyrax Penguin Rottweiler Royal-Penguin Saint-Bernard Salamander Sand-Lizard Saola Scorpion Scorpion-Fish Sea-Dragon Sea-Lion Sea-Otter Sea-Slug Sea-Squirt Sea-Turtle Sea-Urchin Seahorse Seal Serval Sheep Shih-Tzu Shrimp Siamese Fighting-Fish Husky Siberian-Tiger Skunk Sloth Slow-Worm Snail Snake Turtle Snowshoe Snowy-Owl Somali Tiger Toad Sparrow Spectacled-Bear Sperm-Whale Spider-Monkey Dogfish Sponge Squid Squirrel Elephant Stag-Beetle Starfish Stick-Insect Stingray Stoat Frog Elephant Rhinoceros Sumatran-Tiger Sun-Bear Swan Tang Tapir Tarsier Tasmanian-Devil Tawny-Owl Termite Tetra Thorny-Devil Mastiff Tiffany Tiger Tiger-Salamander Tortoise Toucan Tree-Frog Tropicbird Tuatara Turkey Turkish-Angora Uakari Uguisu Umbrellabird Vaonfly Drever Duck Dugong Dunker Dolphin Crocodile Eagle Earwig Echidna Edible-Frog Egyptian-Mau Electric-Eel Elephant Elephant-Seal Elephant-Shrew Emu Eskimo-Dog Falcon Fennec-Fox Ferret Field-Spaniel Fin-Whale Finnish-Spitz Fish Fishing-Cat Flamingo Flounder Fossa Fox Fox-Terrier French-Bulldog Frigatebird Frog Fur-Seal Gar Geck Gerbil Pinscher Gharial Land-Snail Giant-Clam Giant-Panda-Bear Giant-Schnauzer Gibbon Gila-Monster Giraffe Glass-Lizard Glow-Worm Goat Golden-Oriole Goose Gopher Gorilla Grasshopper Great-Dane White-Shark Mountain-Dog Grey-Seal Greyhound Grizzly-Bear Grouse Guinea-Fowl Guinea-Pig Guppy Hamster Hare Harrier Havanese Hedgehog Hermit-Crab Heron Hippopotamus Honey-Bee Horn-Shark Horned-Frog Horse Hummingbird Hyena Ibis Hound Iguana Impala Rhinoceros Tortoise Setter WolfHound Jack-Russel Jackal Jaguar Chin Macaque Rhinoceros Javanese Jellyfish Kakapo Kangaroo Toucan Whale King-Crab King-Penguin Kingfisher Kiwi Koala Kudu Labradoodle Ladybird Gecko Lemming Lemur Leopard Leopard-Cat Leopard-Seal Liger Lion Lionfish Lizard Llama Lobster Lynx Macaw Magpie Maine-Coon Malayan-Civet Malayan-Tiger Maltese Manatee Mandrill Manta-Ray Marine-Toad Markhor Marsh-Frog Masked-Palm-Civet Mastiff Mayfly Meerkat Millipede Minke-Whale Mole Molly Mongoose Mongrel Monitor-Lizard Monkey Moorhen Moose Moray-Eel Moth Mountain-Lion Mouse Mule Neanderthal Newfoundland Newt Nightingale Norfolk-Terrier Forest Numbat Nurse-Shark Ocelot Octopus Okapi Olm Opossum Orang-utan Ostrich Otter Oyster Pademelon Panther Parrot Peacock Pekingese Pelican Penguin Persian Pheasant Pied-Tamarin Pig Pika Pike Armadillo Piranha Platypus Pointer Polar-Bear Pond-Skater Poodle Pool-Frog Porcupine Possum Prawn Puffer-Fish Puffin Pug Puma Puss-Moth Hippopotamus Pygmy-Marmoset Quail Quetzal Quokka Quoll Rabbit Raccoon Radiated-Tortoise Ragdoll Rat Rattlesnake Tarantula Red-Panda Red-Wolf Tamarin Reindeer Rhinoceros River-Dolphin River-Turtle Robin Rock-Hyrax Penguin Rottweiler Royal-Penguin Saint-Bernard Salamander Sand-Lizard Saola Scorpion Scorpion-Fish Sea-Dragon Sea-Lion Sea-Otter Sea-Slug Sea-Squirt Sea-Turtle Sea-Urchin Seahorse Seal Serval Sheep Shih-Tzu Shrimp Siamese Fighting-Fish Husky Siberian-Tiger Skunk Sloth Slow-Worm Snail Snake Turtle Snowshoe Snowy-Owl Somali Tiger Toad Sparrow Spectacled-Bear Sperm-Whale Spider-Monkey Dogfish Sponge Squid Squirrel Elephant Stag-Beetle Starfish Stick-Insect Stingray Stoat Frog Elephant Rhinoceros Sumatran-Tiger Sun-Bear Swan Tang Tapir Tarsier Tasmanian-Devil Tawny-Owl Termite Tetra Thorny-Devil Mastiff Tiffany Tiger Tiger-Salamander Tortoise Toucan Tree-Frog Tropicbird Tuatara Turkey Turkish-Angora Uakari Uguisu Umbrellabird Vampire-Bat Vervet-Monkey Vulture Wallaby Walrus Warthog Wasp Water-Buffalo Water-Dragon Water-Vole Weasel Gorilla Whippet Capuchin Rhinoceros White-Tiger Wild-Boar Wildebeest Wolf Wolverine Wombat Woodlouse Woodpecker Mammoth Wrasse Yak Zebra Shark-Zebra Zebu Zonkey Zorse deer panda giraffe goat gorilla pig hippopotamus horse kangaroo bat rabbit rhinoceros deer sheep zebra lion tiger cheetah leopard hyena",
                                                "Afghanistan Albania Algeria Andorra Angola Antigua Argentina Armenia Aruba Australia Austria Azerbaijan Bahamas Bahrain Bangladesh Barbados Barbuda Belarus Belgium Belize Benin Bhutan Bolivia Bosnia Botswana Brazil Brunei Bulgaria Burkina-Faso Burma Burundi Cambodia Cameroon Canada Cape-Verde Chad Chile China Colombia Comoros Costa-Rica Croatia Cuba Curacao Cyprus Czech-Republic Denmark Djibouti Dominica East-Timor Ecuador Egypt El-Salvador Eritrea Estonia Ethiopia Fiji Finland France Gabon Gambia Georgia Germany Ghana Greece Grenada Guatemala Guinea Guyana Haiti Herzegovina Holy-See Honduras Hong-Kong Hungary Iceland India Indonesia Iran Iraq Ireland Israel Italy Jamaica Japan Jordan Kazakhstan Kenya Kiribati North-Korea South-Korea Kosovo Kuwait Kyrgyzstan Laos Latvia Lebanon Lesotho Liberia Libya Liechtenstein Lithuania Luxembourg Macau Macedonia Madagascar Malawi Malaysia Maldives Mali Malta Marshall-Islands Mauritania Mauritius Mexico Micronesia Moldova Monaco Mongolia Montenegro Morocco Mozambique Namibia Nauru Nepal Netherlands New-Zealand Nicaragua Niger Nigeria North-Korea Norway Oman Pakistan Palau Palestinian Panama Paraguay Peru Philippines Poland Portugal Qatar Romania Russia Rwanda Saint-Lucia Samoa San-Marino Saudi-Arabia Senegal Serbia Seychelles Sierra-Leone Singapore Sint-Maarten Slovakia Slovenia Solomon Somalia South-Africa South-Korea South-Sudan Spain Sri-Lanka Sudan Suriname Swaziland Sweden Switzerland Syria Taiwan Tajikistan Tanzania Thailand Togo Tonga Trinidad Tobago Tunisia Turkey Turkmenistan Tuvalu Uganda Ukraine UAE UK Uruguay Uzbekistan Vanuatu Venezuela Vietnam Yemen Zambia Zimbabwe",
                                                "Actinium Aluminium Americium Antimony Argon Arsenic Astatine Barium Berkelium Beryllium Bismuth Bohrium Boron Bromine Cadmium Caesium Calcium Californium Carbon Cerium Chlorine Chromium Cobalt Copernicium Copper Curium Darmstadtium Dubnium Dysprosium Einsteinium Erbium Europium Fermium Flerovium Fluorine Francium Gadolinium Gallium Germanium Gold Hafnium Hassium Helium Holmium Hydrogen Indium Iodine Iridium Iron Krypton Lanthanum Lawrencium Lead Lithium Livermorium Lutetium Magnesium Manganese Meitnerium Mendelevium Mercury Molybdenum Neodymium Neon Neptunium Nickel Niobium Nitrogen Nobelium Osmium Oxygen Palladium Phosphorus Platinum Plutonium Polonium Potassium Praseodymium Promethium Protactinium Radium Radon Rhenium Rhodium Roentgenium Rubidium Ruthenium Rutherfordium Samarium Scandium Seaborgium Selenium Silicon Silver Sodium Strontium Sulfur Tantalum Technetium Tellurium Terbium Thallium Thorium Thulium Tin Titanium Tungsten Ununoctium Ununpentium Ununseptium Ununtrium Uranium Vanadium Xenon Ytterbium Yttrium Zinc Zirconium"
                                                ,"A-Wednesday Aa-Ab-Laut-Chalen Aa-Dekhen-Zara Aa-Gale-Lag-Ja Aa-Gale-Lag-Ja Aaag-Hi-Aag Aaaina Aabe-Hayat Aabe-Hayat Aabra-Ka-Daabra Aabroo Aabroo Aabshar Aadamkhor Aadamkhor-Daayan Aadamkhor-Hasina Aadat-Se-Majboor Aadha-Din-Aadhi-Raat Aadhaarstambha Aadharshila Aadhi-Raat Aadhi-Raat Aadhi-Raat-Ke-Baad Aadhi-Roti Aadhi-Yug Aadmi Aadmi Aadmi Aadmi Aadmi-Aur-Apsara Aadmi-Aur-Insaan Aadmi-Khilona-Hai Aadmi-Sadak-Ka Aadu-Puli-Atham Aadum-Koothu Aafat Aag Aag Aag Aag-Aandhi-Aur-Toofan Aag-Aur-Chingari Aag-Aur-Daag Aag-Aur-Shola Aag-Aur-Tezaab Aag-Hi-Aag Aag-Ka-Dariya Aag-Ka-Gola Aag-Ka-Toofan Aag-Ke-Sholay Aag-Ke-Sholay Aag-Lago-Do-Sawan-Ko Aag-Se-Khelenge Aagaman Aage-Badho Aage-Kadam Aage-Ki-Soch Aage-Mode-Hai Aagey-Se-Right Aaghaaz Aaghat Aah Aahat Aahuti Aahuti Aai-Shappath..! Aaina Aaina Aaina Aaj Aaj-Aur-Kal Aaj-Aur-Kal Aaj-Ka-Arjun Aaj-Ka-Dada Aaj-Ka-Daur Aaj-Ka-Fashion-Trend Aaj-Ka-Gunda Aaj-Ka-Gundaraj Aaj-Ka-Hindustan Aaj-Ka-Mahatma Aaj-Ka-Mla-Ramavatar Aaj-Ka-Ravan Aaj-Ka-Robinhood Aaj-Ka-Samson Aaj-Ka-Yeh-Ghar Aaj-Kal Aaj-Ke-Angaarey Aaj-Ke-Shahanshah Aaj-Ki-Aurat Aaj-Ki-Awaaz Aaj-Ki-Baat Aaj-Ki-Dhara Aaj-Ki-Duniya Aaj-Ki-Fariyaad Aaj-Ki-Raat Aaj-Ki-Taaqat Aaj-Ki-Taza-Khabar Aaj-Phir-Jeene-Ki-Tamanna-Hai Aaja-Meri-Bahon-Mein Aaja-Meri-Jaan Aaja-Nachle Aaja-Re-O-Sajna Aaja-Sanam Aaja-Sanam Aaja-Sanam-Aagossh-Mein Aakali-Rajyam Aakarshan Aakash Aakash-Deep Aakhari-Decision Aakheer Aakhir-Kyon Aakhri-Adalat Aakhri-Baazi Aakhri-Cheekh Aakhri-Chetawani Aakhri-Dacait Aakhri-Daku Aakhri-Dao Aakhri-Dao Aakhri-Deal Aakhri-Ghulam Aakhri-Goli Aakhri-Insaaf Aakhri-Intequam Aakhri-Kasam Aakhri-Khat Aakhri-Mujra Aakhri-Muqabla Aakhri-Nishchay Aakhri-Raasta Aakhri-Raat Aakhri-Sanghursh Aakhri-Sangram Aakraman Aakrandan Aakrant Aakrosh Aakrosh Aakrosh Aalaap Aalavandhan Aalha-Udal Aalingan Aamdani-Athanni-Kharcha-Rupaiya Aamhi-Asu-Ladke Aamir Aamne-Samne Aamne-Samne Aamras Aan Aan-Men-at-Work Aan-Aur-Shaan Aan-Baan Aan-Baan Aan-Milo-Sajna Aanch Aanchal Aanchal Aanchal-Ke-Phool Aandhi Aandhi Aandhi-Aur-Toofan Aandhi-Toofan Aandhiyan Aandhiyan Aangan Aangan Aangan-Ki-Kali Aankh-Ka-Nasha Aankh-Ka-Tara Aankh-Ki-Sharam Aankh-Micholi Aankh-Micholi Aankhen Aankhen Aankhen Aankhen Aankhen-2 Aankhin-Dekhi Aankhon-Annkhon-Mein Aankhon-Mein-Tum-Ho Aansoo Aansoo-Aur-Muskan Aansoo-Ban-Gaye-Phool Aansoo-Bane-Angaarey Aantiya Aao-Pyar-Karen Aao-Wish-Karein Aap-Beeti Aap-Jaisa-Koi-Nahin Aap-Ka-Pyar Aap-Ka-Surroor Aap-Ke-Deewane Aap-Ke-Liye-Hum Aap-Ke-Sath Aap-Ki-Kasam Aap-Ki-Khatir Aap-Ki-Khatir Aap-Ki-Marzi Aap-To-Aise-Na-The Aapas-Ki-Baat Aaplee-Maanse Aar-Paar Aar-Paar Aar-Ya-Paar Aarakshan Aaram Aarambh Aarohan Aarop Aarti Aarya Aarzoo Aas Aas-Ka-Panchhi Aas-Paas Aasha Aashayein Aashiana Aashiana Aashiq Aashiq Aashiq-Awara Aashiq-Banaya-Aapne Aashiqui Aashiqui-2 Aasma Aasman Aasman Aasman-Mahal Aasman-Se-Gira Aasman-Se-Ooncha Aasra Aastha Aatank Aatank-Hi-Aatank Aatankraj Aatish Aatish Aatma Aatma Aavishkaar Aaya-Toofan Aaye-Din-Bahar-Ke Aayee-Phir-Bahar Aayega-Aanewala Aayi-Yaad-Teri Aazaan Aazadi-Ki-Ore Aazimaish Aazmaish Ab-Aayega-Mazaa Ab-Insaf-Hoga Ab-Ke-Baras Ab-Kya-Hoga Ab-Meri-Baari Ab-Tak-Chhappan Ab-Tak-Chhappan-2 Ab-To-Jeene-Do Ab-Tumhari-Bari ABCD Abdullah Abdullah Abhay Abhi-Abhi Abhi-To-Jee-Lein Abhi-To-Raat-Hai Abhilasha Abhimaan Abhimaan Abhimanyu Abhimanyu Abhinetri Abhinetri Abodh Abu-Kaliya Accident Achanak Achanak Achcha-Bura Achcha-Bura Achchaji Achhut Achhut-Kanya Acid-Factory Action-Jackson Action-Replayy Actor Actress Adaab-Arz Adalat Adalat Adalat Adharm Adharm Adhikar Adhikar Adhikar Adhikar Adhuri-Kahani Adhuri-Suhag-Raat Adi-Manav Adil-E-Jahangir Adil-E-Jahangir Adisaya-Piravi Admi-Aur-Aurat Admissions-Open Adutha-Varisu Adutha-Veetu-Penn Advaitham Ae-Dil-Hai-Mushkil Aetbaar Aflatoon Aflatoon Aflatoon Aflatoon Aflatoon-Aurat Afra-Tafree Africa Afsana Afsana Afsana-Do-Dil-Ka Afsana-Pyar-Ka Afsar Afterword Afzal Agabai-Arechcha Agar Agar-Tum-Na-Hote Age-Badho Agent-123 Agent-777 Agent-Vinod Agent-Vinod Aggar Agnee Agnee-Pareeksha Agneepath Agneepath Agni-The-Fire Agni-Chakra Agni-Morcha Agni-Nakshatram Agni-Prem Agni-Putra Agni-Rekha Agni-Sakshi Agni-Varsha Agnikaal Agnipankh Agnisakshi Agosh Agra-Ka-Daabra Agra-Road Agreement Agyaat Aham-Premasmi Ahankaar Ahinsa Ahinsa Ahista-Ahista Ahista-Ahista Ahsaas Ailo-Bailo-Sailo Air-Mail Airlift Aisa-Bhi-Hota-Hai Aisa-Kyon-Hota-Hai Aisa-Pyar-Kahan Aisaa-Kyon Aisha Aishwarya Aisi-Bhi-Kya-Jaldi-Hai Aisi-Deewangi Aitbaar Aithe Aitraaz Aiye Aiyyaa Aja Ajab-Gazabb-Love Ajab-Prem-Ki-Ghazab-Kahani Ajamil Ajay Ajee-Bus-Shukriya Ajeeb-Ittefaq Ajeeb-Ladki Ajit Ajnabee Ajnabi Ajnabi Ajnabi-Saaya Ajnabi-Saaya Ajooba Ajooba-Kudrat-Ka AK-47 AkaashVani Akadd Akalmand Akalmand Akash-Pari Akayla Akela Akela Akele-Hum-Akele-Tum Akele-Na-Jana Akeli-Mat-Jaiyo Akhri-Badla Akhri-Muqabla Akira Akkad-Bakkad-Bam-Be-Bo Akriet Aks Aksar Al-Hilal Alaap Aladin Alag Alag-Alag Alai-Payuthey Alakh-Niranjan Alakh-Niranjan Alam-Ara Alam-Ara Alam-Ara-Ki-Beti Alamara Albela Albela Albela Albela-Mastana Albeli Albeli Alibaba Alif-Laila Aligarh All-Alone All-Is-Well All-Rounder All-The-Best Allah-Ke-Banday Allah-Rakha Allaudin-Ka-Beta Allaudin-Ka-Chirag Allaudin-Laila Allaudin-Laila Alone Aloo-Chaat Always-Kabhi-Kabhi Amaanat Amaanat Amaanat Amaanat Aman Amanush Amar Amar-Akbar-Anthony Amar-Deep Amar-Deep Amar-Jyoti Amar-Jyoti Amar-Kahani Amar-Prem Amar-Prem Amar-Prem Amar-Prem Amar-Pyar Amar-Rahe-Yeh-Pyar Amar-Saigal Amar-Shaheed Amar-Shakti Amar-Singh-Rathod Amara-Prema Amavas Amavas-Ki-Raat Amba Amber Amchya-Sarkhe-Amhich Amdavad-Junction Ameer Ameer-Admi-Garib-Admi Ameer-Garib Ameer-Zadi Ameeri American-Chai Amiri-Garibi Amma Amma Amma Ammaa-Ki-Boli Amrapali Amrit-Manthan Anamika Anand Anand-Ashram Anand-Aur-Anand Anand-Bhavan Anand-Math Ananda-Jyoti Anandigopal Anari Anarkali Andaaz Andar-Bahar Andaz Andaz-Apna-Apna Andaz-Naya-Naya Andha-Intequam Andha-Kanoon Andha-Yudh Andhera Andhergardi Andheri-Raaton-Mein Andhi-Gali Andhon-Ka-Sahara Andhon-Ki-Duniya Andolan Andolan Andolan Ang-Se-Ang-Lagale Angaar Angaar-Vadee Angaara Angaarey Angarakshak Angoor Angoori Angoothi Angry-Young-Man Angulimal Anhoni Anhoni Anita Anjaam Anjaam Anjaam Anjaam Anjaam-Khuda-Jane Anjaan Anjaan Anjaan Anjaan Anjaan-Hai-Koi Anjaan-Parindey Anjaan-Rahen Anjaana Anjaana Anjaana-Anjaani Anjaane Anjaane Anjali Anjali Anjan-Garh Anjane-Mein Anji Anjuman Ankahee Ankahee Ankh-Ka-Nasha Ankhiyon-Se-Goli-Maare Ankhon-Dekhi Ankush Ankush Annadata Annarth Anokha Anokha-Andaz Anokha-Anubhav Anokha-Bandhan Anokha-Daan Anokha-Insaan Anokha-Milan Anokha-Modh Anokha-Prem Anokha-Prem-Yudh Anokha-Pyar Anokha-Rishta Anokhi-Ada Anokhi-Ada Anokhi-Mohabbat Anokhi-Pehchan Anokhi-Raat Anokhi-Seva Anpadh Ansh Ansoo-Ki-Duniya Antaheen Antar-Mahal Antardwand Antarnaad Anth Anthahpuram Antharangam Anthony-Kaun-Hai Antim-Nyay Anubhav Anuradha Anurodh Anwar Anwar-Ka-Ajab-Kissa Anyay-Hi-Anyay Apaharan Aparichit Apartment Apatkaal Aplam-Chaplam Apmaan Apmaan-Ki-Aag Apna-Asmaan Apna-Bana-Lo Apna-Banake-Dekho Apna-Desh Apna-Desh Apna-Desh-Paraye-Log Apna-Dushman Apna-Ghar Apna-Ghar Apna-Ghar-Apni-Kahani Apna-Hath-Jagannath Apna-Jahan Apna-Kaun Apna-Khoon Apna-Paraya Apna-Sapna-Money-Money Apna-Sapna-Money-Money-2 Apnapan Apne Apne-Apne Apne-Begaane Apne-Dam-Par Apne-Dushman Apne-Hue-Paraye Apne-Paray Apne-Rang-Hazar Apni-Chhaya Apni-Izzat Apni-Nagaria Apradhi-Kaun Apradhinee April-Fool Apsara Arab-Ka-Lal Arab-Ka-Saudagar Arab-Ka-Sitara Arabian-Night Aradhana Armaan Army Arpan Arth Arunachalam Aryan Arzoo Asambhav Asha Ata-Pata-Laapata Athadu Athidhi Atishbaaz Atithee Atithi-Tum-Kab-Jaoge Atma-Tarang Atma-Vishwas Atmaram Atyachar Aulad Mumbai-Can-Dance-Saala Take-It-Easy Tevar Her-Story I Alone Sharafat-Gayi-Tel-Lene Baby Dolly-Ki-Doli Khamoshiyaan Hawaizaada Rahasya Shamitabh Bachpan-Ek-Dhokha Jai-Jawaan-Jai-Kisaan Roy Badlapur Qissa Ab-Tak-Chhappan-2 Dum-Laga-Ke-Haisha Dirty-Politics Badmashiyan Coffee-Bloom NH10 Luckhnowi-Ishq Dilliwaali-Zaalim-Girlfriend Detective-Byomkesh-Bakshy Doctor-I-Love-You Paisa-Ho-Paisa Tere-Ishq-Mein-Qurbaan Ek-Paheli-Leela Dharam-Sankat-Mein Barefoot-To-Goa Mr-X Margarita-With-A-Straw Ek-Adhbut-Dakshina Court NH-8 Jai-Ho-Democracy Ishq-Ke-Parindey Gabbar-Is-Back Sabki-Bajegi-Band Piku Kuch-Kuch-Locha-Hai Makad-Jaala Bombay-Velvet Lateef Ishqedarriyaan Tanu-Weds-Manu-Returns Welcome-To-Karachi P-Se-PM-Tak"
                                                ,"It-Follows Insidious-3 Into-the-Woods In-the-Heart-of-the-Sea Interstellar Inner-Demons Iceman If-I-Stay Into-the-Storm Inside-Llewyn-Davis Insidious-2 Igor Iron-Man-3 Ice-Age-4 Immortals In-Time I-Am-Number-Four Inception Iron-Man-2 Inglourious-Basterds Ice-Age-3 Iron-Maiden Invictus In-the-Name-of-the-Kin Get-Hard Good-Kill Gone-Girl Guardians-of-the-Galaxy Get-on-Up Grace-of-Monaco Godzilla Gravity GI-Joe-Retaliation Gangster-Squad Gandhi-Park Ghost-Rider-Spirit-of-Vengeance Green-Lantern Gullivers-Travels Going-the-Distance Geng-The-Adventure-Begins Grown-Ups Green-Zone G-Force Ghosts-of-Girlfriends-Past Gran-Torino Get-Smart Spectre San-Andreas Seventh-Son Star-Wars-The-Force-Awakens Sex-Tape Swim-Little-Fish-Swim Space-Station-76 Sin-City-A-Dame-to-Kill-For Step-Up-All-In Son-of-God Sabotage Saving-Mr-Banks Shes-Out-of-My-League Star-Trek-Into-Darkness Saint-Dracula-3D Snitch Silver-Linings-Playbook Stolen Skyfall Special-Forces Shark-Night Step-Up-Revolution Street-Dance-2 Snow-White-and-the-Huntsman Safe Sparkle Safe-House A-Game-of-Shadows"
                                            };
        public static string category;
        public static string[] array;
        public static Random rand = new Random();
        public static int ind;
        public static string quest;
        public static string temp;
        public static string already = "";
        public static int wrong = 0, correct = 0;
        static string alp = "abcdefghijklmnopqrstuvwxyz";
        static char[] alphabets = alp.ToCharArray();
        Frame rootFra = Window.Current.Content as Frame;
        string attempt;
        private void solve()
        {
            if (wrong == 6)
            {
                showP.Begin();
            }
            else if (correct == quest.Length)
            {
                rootFra.Navigate(typeof(BlankPage1), "win");
            }
            else
            {
                if (attempt.Length != 0)
                {
                    Block.Text = display()[wrong];
                    int[] indx = check(attempt.ToLower().ToCharArray()[0], quest);
                    if (indx[0] == -1)
                    {
                        wrong += 1;
                        if (wrong != 5) errorBlock.Text = "Try something else!";
                        else errorBlock.Text = "Last chance!";
                        msgPopup.IsOpen = true;
                        show.Begin();
                        already += attempt.ToLower();
                        Block.Text = display()[wrong];
                        if (wrong == 6)
                        {
                            errorBlock.Text = "You Lose!";
                            showP.Begin();
                        }
                    }
                    else
                    {
                        for (int i1 = 0; i1 < indx.Length; i1++)
                        {
                            temp = temp.Remove(2 * indx[i1], 1);
                            temp = temp.Insert(2 * indx[i1], attempt.ToLower());
                            question.Text = temp;
                            correct += 1;
                            score = prevScore + correct;
                            already += attempt.ToLower();
                            errorBlock.Text = "";
                            if (correct == quest.Length)
                            {
                                rootFra.Navigate(typeof(BlankPage1),"win");
                            }
                        }
                    }
                }
            }

            attempt = "";
        }

        private void Button_Click_f(object sender, RoutedEventArgs e)
        {
            rootFra.Navigate(typeof(HomePage), "a");
        }
        static string x1 = "No";
        private void Button_Click_k(object sender, RoutedEventArgs e)
        {
            rootFra.Navigate(typeof(HangMan.KnowPage));
            x1 = "Yes";
        }

        private void a_Click(object sender, RoutedEventArgs e)
        {
            if (wrong != 6)
            {
                var button = sender as Button;
                button.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                attempt = button.Name;
                vis[alp.IndexOf(button.Name)] = 1;
                show.Stop();
                msgPopup.Opacity = 0;
                solve();
                int x = 6 - wrong;
                chances.Text = "Chances Left : " + x;
                scoreBlock.Text = "Score : " + score;
            }
        }

        private void show_Completed(object sender, object e)
        {
            if (errorBlock.Text != "Last chance!" && errorBlock.Text != "You Lose!")
                msgPopup.IsOpen = false;
        }

        private void showP_Completed(object sender, object e)
        {
            rootFra.Navigate(typeof(BlankPage1), "lose");
        }
    }
}
