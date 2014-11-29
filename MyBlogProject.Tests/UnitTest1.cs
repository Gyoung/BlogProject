using System;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBlogProject.DataCore.EntityFramework;

namespace MyBlogProject.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CreateDB("aa.db");
            //BlogDbContext context=new BlogDbContext();
            //var aa = context.Blogs.FirstOrDefault();
        }

        public static void CreateDB(string dbPath)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + dbPath))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "CREATE TABLE Demo(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE)";
                    command.ExecuteNonQuery();
                    command.CommandText = "DROP TABLE Demo";
                    command.ExecuteNonQuery();
                }
            }
        }

         [TestMethod]
        public void TestSqlite()
        {
            ModelContext context=new ModelContext();
            context.Orders.AddObject(new Order(){Name = "123"});
            context.SaveChanges();
        }
        [TestMethod]
         public void MainTest()
         {
            //Image im=new Bitmap("10.png");
            //BinaryFormatter binFormatter = new BinaryFormatter();
            //MemoryStream memStream = new MemoryStream();
            //binFormatter.Serialize(memStream, im);
            //byte[] bytes = memStream.GetBuffer();
            //string base64 = Convert.ToBase64String(bytes);
            
            var data1 = "iVBORw0KGgoAAAANSUhEUgAAAhkAAACFCAYAAAD2IeqaAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAB4HSURBVHhe7d0JeFTV3QbwFwYmCZOEbGRfIQtEEFkDYceKFBRQAQVFVIQqIh+tVhSr4AJWKi0flaViEbUFxI+6FBBQiyL7lgIhEJYkJCH7vmfIwDcDF2WZOXdmMncyk7y%2f55nHcxN1Mut971n+p9Xg+6ZcAREREZGNtZb+SURERGRTDBlERESkCIYMIiIiUgRDBhERESmCIYOIiIgUwZBBREREimDIICIiIkUwZBAREZEiGDKIiIhIEQwZREREpAiGDCIiIlIEQwYREREpgiGDiIiIFMGQQURERIpgyCAiIiJFMGQQERGRIhgyiIiISBEMGURERKQIhgwiIiJSBEMGERERKYIhg4iIiBTBkEFERESKYMggIiIiRTBkEBERkSIYMoiIiEgRDBlERESkCIYMIiIiUgRDBhERESmCIYOIiIgUwZBBREREimDIICIiIkUwZBAREZEiGDKIiIhIEQwZREREpAiGDCIiIlIEQwYREREpgiGDiIiIFMGQQURERIpgyCAiIiJFMGQQERGRIhgyiIiISBEMGURERKQIhgwiIiJSBEMGERERKYIhg4iIiBTBkEFERESKYMggIiIiRTBkEBERkSIYMoiIiEgRDBlERESkCIYMIiIiUgRDBhERESmCIYOIiIgUwZBBREREimDIIKLmR92AgeFleCvhHDbevxer46SfE5FdtRp835QrUpuImoibaz1GhZdjUGApwnzK4da2Bpq2Oum31zTUe6Be%2f6Payg7IL%2ffA8QJ3fJPnigta6V9owdxcL2FIcDmGBxajU0AhvN3qoZJ+Z5B25G48mSodEJHdMGQQNZnLGBSbj2kx5xHe%2fuaTovnaoK40FMfOB2PJeTfk35xLWoyhvZPwRmyJdHQ7hgxbuIKnBu7B1PB66dgYF6QeHIAZ51pJxzYSlo0tg1LhLh0aU3omEeMOu0lH5Cg4XEJkd1cQF5mL9Q%2fswtu9UxBldcAwaICrdwYSeu%2fFxvFHsTq+Du2l3xDZViusyQmDKGJA%2f9vIwErY+lQ%2fJTxDGDAAfySnMWA4IoYMIntS1+DtXx3EysQUBLvZuNtBVYrY+Dw8ZH1iIRJL88XpGqltgktgCSbZ9D1YgcQgcbRBfjBWmO7IoibEkEFkJ26+xfh49EEM8q9qRM+FWOH5IKyxKrtcQUBgBd5KPIktg8qknxHdyh07cj2ltgnqbPSLst0ovFt0CTqppQOjVMjM9UKOdESOhSGDyA7cAvPw6a%2f+i0hb917cSBuJnaddpAPzGCacPtQtCxvH7cLG4YcwODIP7uwJIYHNGaEoldrG2XLI5AomBWZD+K42vO9T+aZ1VAwZRAoz9GB8MOAkOij8PViaEYzltdKB0GV0Dy%2fC8nsPYvODuzG72xkEtGuQfkckI98HyTJDEzYbMlFVol+gzCyQXB8re+%2fIHhgyiJTkVo73h%2f4X4eZ2MOg8kJ%2fRDet3D8D0fw3HkHV3%2f3wb89UgzPouAetPdUZ2qRtuigX6q7lvT5p57RiQjzcGHkNX30q0kX5EZD4XbMr3l9omqLPRJ0JqN4JbdCGihUMlnkjJkBm+oSbFkEGkmAY8l3AS0eYEjMvuyDnTFzP+ry8m7vXHqkxXnKm7eRlgebUaJwrcsSopBI9+k4j7NvfDjmwvGC7izO%2fFIGq8pNPByJbaxtWjU1CF1LaWDs+FZInnL1WE4suLUpscEkMGkUKiuqRjbLAZZ%2f76MGz9ti8mHfZAqgXdvrUVGizc1RPPHuqJTeb2YhDZQq0XkvLE4yEuQaWYIrWt4laO7v7iD0ThRR%2f8ILXJMTFkEClB%2fwX5h%2fhM8YQ1g%2fpIbNwei3eLrS1e1AqpZ73xKXsxyK5UWJcXfLUXzSR1FhI7Sm0rBEcVIUR4hgrFIQsnOpP9MWQQKWDoHWnywySX%2fXBgT0csr5KOiZxITmogzglL2jdmyESHKUHioRJdXgeGayfAkEFka25lmBopVxnIBRnH7sBLeTYuv0xkLzoP7M8TJ2mrh0z0n6E7A6S2US44l+nN2hhOwLn3LlH7oNfExzF6UCeE+7jDXf1L7r1UV43ahnqU5OQgI+kQfti+B6eL7biTlHsMhjzxAO7pFgZ%2fLw08XX+Zx6%2fT1qC6pgoF6WnYu30DvttXAsv%2fMjW8+4%2fAuGHdEdcpFD7uLjfdh%2f5OUFV7CdVl+chKO4FD23diX7I192OC%2frnvPOzXGDG4GzpFtodG7XrT84+GOlTU1aOysBi5WY2%2ff7VvF%2fQfdy8Se0YiyssVLm7t4HLD3V19vfW3goICZDfF632DrnedxPL4POnIOF3+XZj2vS%2fSpWO7CsjFl3enwFs6vE1OLwz5wUs6sAOVDnd3KsBDYYWI9KyA642bm+ncUF3vjtKCDjiS7Yu%2fZ6pRLv3qRs6%2fd0lPzF47B4lGXpSiA0sxa9FR6cjBdLyAHf3OCYYFXZCyfyCeTZMOzST7GdJ2wsdfRHLpqhNwwJAxEW9+dR9ipaPblB7Fu0+sQPGYmZj1SHeEa0QdajdqQEXeeezb+A%2f88%2fsLlp%2fspryFDeNNr8n6+YtAHYERL87EhN5B8DDrT6vDsdUz8M5m6VCO%2fv8%2fcMZjeCgxGkFmP%2fZrLlUVIGXf1%2fjnB7uQae35Vx+eRs2ehnG9guFp4fpHnbYUWSePY%2fMnn2J3mnl%2fgKbzfZj2%2fGj0CdWgrfQz8+hQX5KPE0e2YmNjHq%2fF6vH2yN0Y5CMdGuWJpB%2f7YI69ZsXLhQprVMRj%2fuYg45PuzA4xlzEo%2fgJe7JYGLzPfyjnJgzHp+O3vBIaMplKNpWP3o4dGOjSi%2fkIfjNhjyTJT+c9Qxfl+uP+A4E7JYTjhcElrxM59Fwun9bQgYBi0gWdgHO6dvQDLV8zBiI7CxdfWCRmDVz9cgKcSzA0YeoX%2fxSazAoYa4WPmYPHaBZh1T5zFAcOgrbs%2fut%2fzNN5Z+y5mj4nQ%2fx8toxk6E4s%2fmIfHEywPGAYqtTciewzBrL8swex+0g9N0qD%2fC+9i6aKJSLQ4YBio4OITjN76x7v4gzn6r3A78apArDBg6BVG4X9b+rI7VS0WjNiPt+8yP2AYrl6%2fPWn5O4GUpMF3eeIAYfGQiU8ZOgs%2fQz44lcGA4SycL2R434UHEn2tOOlcp4JHSE889Sf9iXaoDd+omsF488%2fj0a29JSd%2fLc78+BnOSEemBeHuBdYEK+NUmiAkTpuPpQvHIFD6mZzACa9hiT4Z2OL+UXgK2%2fZLbaOCMPqdxZg12IKwJpB34hvY6zowOLQEwqFk%2ffsv86J30wyTOIpWtVh87wEM87Ns1l5pRgC7xx2QbJlxC1eZ9IgoQAepbVRJMNbnS21yeC134mcbXyTOXoTZA2zTo+HXtSdiXaUDc5Um44tPi6UDE9Rd8Pj7b2B6j8YEK2PawKfreCz680T5oBEyGbMmxph%2fxSkj47A4WAU+OQsT4z2EM8vNl4ujn9ivn%2fx+n1ypZUoQktNt9EQ6q6AUJHhZmBYsqWhK9iVbZtySVSa1mBRSILWNy872Q5LUJsfXsleXqLz1QeM1jA+Rju1Kh%2fQ9n8p8WIIw%2fo+%2fxagwS9OL+dp1+jXmzesrHDpJnD5QprSvBXQXcWKTKFgNxKN3h8nXlzCTLisZmwulA8XVIqa9zMmz2AdbuOzOYhVZgaxo6rBcsCFPfKli9pBJSAm6iEZfLoch6XwLD+lOxmlDhq66CMe+%2fQgLn30Wj4x9%2fNptwhy8+OJ7WPnZfhzOLkW9ORdLrhEYNWuUxfMT5Oiqc3Ho85V48amnr%2f1tj76Ev3yyHyll0o4Tlafw79Wik60afea9jAc6yQSMhnJkJP2IlW8uwP88Kj0P+tvjT72KhSu2Y2+a3POggn%2fC0%2fjDlCDp+FajMOwOd6ltgq4Sp818LerP7sPnopP+A8Nwp4fUNkFXno7tK9677fG+8uZH2LArGRklWqlIkBanf%2fwMcotJbacevjJ%2fe1W5G5KlNkl03jhzIgGvfjXs2j4tnw3FC7t7IKnA%2fdrrqD+x%2fHicY%2fCOLDk1UFxm3Mwhk%2ftCcyDKGPXZDJvOxvlWl+i%2fdspSvsai+V%2fIrxhw747JC2ZgdIxc13sZ9i6ajWUHpENjZFaX%2fEKHyrPf4q1560z8fYYJja%2fjnorFeFMQMtQPvIZVT8SgnXR8O%2f39ZOzGslf%2fjhMyxZzUHUfi+XkT0aeDYLZm3Vms1T%2fGbbf+zQlz8P68nvCTDm9XhxNrZ2HhFzIvxtXXYjICtszFX3ZKPzOix7xlmJsgWD5p6u+8habXY%2fj9tADsmLkEe6WfKc6MVRwZScMx9VQT18ZQegmrBatZGkrjseT7IGw1+npeQVxMJhZ4+WHSIXHI4OqSpqbDi8N%2fwv2Bpq9o5FeZyK1UcUHqwQGYcY61ZZyJ8%2fVklB7D314xI2AYVB3Duhd%2fi6UHSq9dEZnkhV7jRkntxqk5%2fw1ee9FUwDCoxr4lc4UBw%2fCF88xYUcCQ7ud%2f5AOGgTZtG5bMXIW9xYJnwTUGI5+Pkw5uEOwB4cV59XnslgsYBldfC3HAMAjwED1qoDL1J9mAYVB95B9YYM+AYaD%2f7hN%2f%2fbmgpp5fkNfpyuLxzg5TAcPAUDI9QjZgkCNQYfnFMOH3rOyQSVgpYkQvdXUUvmbAcDotYE6GFocW%2fRFfp4vPTC7RvTBSalutLh1fv7MR4jJM8nymT0CC6DKw8iQ+ftnC+9EexLK%2fHUORdGhMYI+xSJTaZmu4hBqpaQ+XtPa8Nwu1r9HHVRENKqulZktnKKl+JAjfidM%2fOZHacx3EZcZlhkweDMmBaGC2Is8L5pYTIsfRQiZ+5uKz9cnCEyzUIbhzhNS2ig4ZO1fhy0ZPMozDxIQQwfCODuk%2ffogfzbiav82BT7EvS%2fCt7hGK3glS+7qMcqMVFn%2fWPhrDRthuRktWmfgs7NN5GIbYegIN2V1FRie8yWWIzYtsmXHRKpMK3BNaKbWN8cexs+zRckYtZ3XJgW+QLAwA7RDao6vUtoIuDyc2yS1fNEP0MMSIFolr07H7Y5llryYV45tk0fIwL0T0j5LakqTjyBae993R8zfv4gUrinsZczIpB6KvGnjcgadXvKRMMTWyE0+cz3JH85+%2fZ5hn8Qk2fGXOzfh8DAO%2fhDlG%2fn3jtzcbtbd6Y7XCmpwwfZQwzdSQiVt0CTqJPtLFgdhgvxncZEMtJ2QgFUcyxGu1%2fQPipZYVCi9gjw2WSvr8KgqiFbX15w5jizW9GJKSE%2fnC1RZ+AV2k1nU%2fYPcZmYkfbXzRZ5qhkupLeLC%2fT+PCxo49OC1MGUDbDl3x1HvLsHjBePTyZdhwOlp%2fHGnpFU+bqzQ%2fpIguSowOmVzBpMBswbJ1FTKzfLgqy0m1oJABHLogrEunP8MHo4fUtFh1CTKkZmP0jfCVWsa5xD9i9ArG7NtLd0FUsdfF5fZR0b3Lv8fpOunAJEMl1a6Y+PISrP7obcx+rDe8rTr%2f78ZHW9Pl53mo2iG8xxj8fvUKvP+XOY0PN7ZwuZXMBGO6qk6NLKlJzY1cmXEjQyaqSvQLFPR%2faCOxM5W1MZxViwoZSCsV10xQuwjXaIsUFZ2TWo0RhTAfB7wyL9yERcuOosCsM6hhz5BwJE6YjZXrV2LJwmkYaOHQRsm6hVgpuyJIolLDr2NPfbhZir%2f%2f8z28MmswwpvqKaxwg7iv7BJcHfDltbsqN+Mbq1GzIFdm%2fNYhE7foQmGxv%2fo8H6xnendaLStkNFzGZalplMZdXDNfcd5ws1WpSxvT7lmKRevPosySD3sbDUK6GjZEW4UVix9DN5maXr+4tiJoXUqlRT0D1zeAW7z+r3j96e76ayo707USv79QifbuDlaWhsjW5MqM3zRkcgVPBWUJJrp7IiXdswXM32m+WlbIcHiB8GjiCdT19abnX+R9%2fhZmz9+G0+WWXla0gU%2fcCLz68V%2fxyiRzJ4jmYssrv8VbX6ZbFmwM2rRH%2fP0vYNXHr2KsPSeIFrmKV+LoebnLjjsROTm5MuM3DJm4laKfoIAXqoPxHefvODWGDIfiDpcm7k4vyj8ltYzTnliHBU8vwJoDOaiQKqSbTX%2fy7%2f7IfCyeK94r5RdanP5oPma%2fuA57s6txSfqpudp6xWHSn97FczbaBE+eCwrF4yXw8qxBsNSm5u4olj3xS+l78W0p9poYYzBU%2fDT+39x+e%2f1T6T9qYnJlxq8PmQRHFSFEcBYqzPRjbQwnx5DhUApQ1aTFmmqQn2LGJuTaC9ix6GXMmPoWPtmViSKtRWMoCEwU7ZVyO0PF0mXPPYtn5m7ED7J7sdyijS8G2W0TPDecr5CZoNahDA9wDhs1d7VeSMoTvNGvDpnoMEU4VBKI5HQHHT8msznf3iWlR%2fHuE0ut2+p3xFysfu4O02WytWexdsJb2CYd3kRm7xLb7C9gev+Cayqwf%2fEsLN0jHToMDaIfmoqpY3ojxkuwP8qNdLnY+vxcfGJNV6h7DEb9ZiruSwyHj7l3l%2f0fvPDc2kZXY5XT9a6TWB4vuhcV0o4MwZOpTVgeuan3Lmns%2f98I7l3ieNzizmNLrwyTIaI+JxKFwRkIlY5vpcvrgcf+44Mc6ZicU4vqyVCHeMrsw1GJpi1CeAHigpee8Itt4vEUo6pxbtMKvDb1Gbz09%2f04e32nWRFVEAZMHygdWKjqLLYu+QNmTnoNa3ZlosScuwvti0eHSQcKSs71Ec6s1391IiKklEMm1OzJlRl3EQQMQxi%2fcNGbAaMZaFEho3+UuAYFSnKs6yGxmWIUVIgrbQVEWby7iB1pkfm1PmxMfwNrkopl51C0jx2AIVLbKoZhG33YmPP7dThUKJc03NF52FCpraB8T2TIDHmpAs%2fj5TCuMqFmTrbMuIA2Ej9xM7RmoQWFjIHo3VG8w+fFzENSq+kcvCAuGe7Rsa%2flm5jZm+Hkv2AuPjwmUylU0wGx0VK7Ea7tMvsJjspUCvUI6IxIqa0cDb68KCp3ZlCBrvFF1hd+I3IKrbDmQiTM2Cj6NhVZHbDG0lVl5JBaTMhQTxiOO4VjJVXIOWbGpEeFlXyXDuE0BY9YjJwi0yPjELT48a8HZaqgquFmq4ei%2fQEf7pbZO8bF1fQ8ARv6IS0YchXmVb4n8VrvWrhJx40V5V+HW3adIWp6Wd44a%2fFkdk+cz+ZmaM1FywgZ6r545tdRgtr4epUZ2LdTajelc9txWjg7UY3Y+2ZipCNOzbhVYQZK7LhapiStRLy5mr2U+GNrplw3sQ6+sUlYEWfpOuDbRUVfwLLhR%2fGHGBtd+nnU4ldSk6hx5MqMG1ESjo9ZG6PZaAEhIwij35iKBF%2fxusG8pK+wV2o3rXRsOnxRXOnSNQaPrpiNPtYEDXUEHn5vIaZbvBecGiMX%2fhmvml1My8AX7VylpqXUo%2fDK6tcsK6bl1w7iATF7aYU1KXHIF5f%2f1KtFxx5H8HHXeit7NC5jULezWN77HDxb1yI6PhcPmrM8tr4NhDN%2fPIowVG7Ex8lo3BqxqyA1yuaz4bI9ezfKz%2fNu4rlxZEvOFzI0oUgcal5XmrrjSMxe9QamxHsI1mLr1Z3Ftr86zvq2ktWf44DMXm5tO%2fTGnA%2ffwORe5ncrano9htdXz8cDMWHoP2WUBWFBr8PDGNLFD90eMey2OsesrdYDn+yJKNETrytF5gGpfQufqYPQ1T8Gkwy7rc4bacZ+JEF4uG+I8HXWFWXZ78urxA8fnDfjTN26CpF37sOmYQUY42nuZNArCAgswer7f8Lb3TKhuf4p1pzDI91EG21LylxQLDznFqBv91Lx8Iu6Bm%2f3K3b8+UESl7Yc4G8yJV44be427ZfDcDDVom8mcnCqiNjuC6S2g7gDwybF6q+BTVBpENF%2fJEYM6wq%2fS+UoKitFRa30BeIeiKiIbkicPB6Tpk3Fk+N7IdJDrpCCFuc2L8EHR2SmJ3UfjvHxptf211zcj60%2fycwJMFsukmpiMbyvP0QdAa1dvRE35Ne4%2f94+CPe7gpqqOlSXVP2yqsPwfHTrg6GTJ+KJGU%2fisZGxCHC9dkZq6+MPzxM7cNTMS4zYZ5%2fEg1GGfoLWcPEMQo+Ro%2fSvQQ9EBbS65X41COg1EGN+8yymDQ5BO0GM1V08jFVbjxvZlyAOU2eORkdDfmrdFu1Du+GeB0egf58Y+LauQXVtFcoqpUepf4zdho3FlBcex9BwV0Fq1uHCvg%2fw%2fVF77YLQCmn5XogPzUGoq1x4uAK1RwESY7MwIViFLioVLmlVKNS2xvXBlPYaLWI7VOPpuBw83%2fc4nozNga%2fLrV0lV+DuexkBGX7YIwwRbdAlMAtxgn1U2njkYmRoW0RoXZBZ2wZXK8mrdPq%2foQbPdD+LlxNOI6atD34643H7vBv3KjzSsdB070xlMNZmWNvFZZy7fwlGdzD9GXZt8MWO9HaOMZxmVBASxvVDmJEnzbbfLU2hDQrca4Wvz3W63BjMOef68%2fuenJ%2fzFeOysZrzmzHvdxvlizTZpRjXjdToM+9PmJPgLe6FaYSalA145pWt4q5zA%2fUYvPbP8bjDphcYdTixdhYWfnH7vasnvI7Vj0WL59BYqu4s1upfw2327jUPyMO%2fhp3UhyPp2A7qLvTBuD3iTaWC48%2fgH3eJqi2aoSIe8zcH3b6jahMU40LcefzYSzDNWH+F%2fO+vYvGevTIm3cytBJ+OTUK48HPggtSDAzCDS1ebFTt+9Tmemqz%2fYPHLZgSMJmHdTqSWaBc3HFPNmJvhM7U%2fOtu4B7Pm7HasNBIwDPM4JgyRmaRrsTqc%2fmqF%2fQOGQX4gHv0hHsWy8zNsxzXiPN4KFF875KSE4bDMPitOpbwdyqSmUa2zMPAOJowmU9sexwpkIm1NJLYxYDQ7LTRk6FCW8gUW%2fG4tTjfFicdshp1IX8L7B+QLW1lF5Y+BU8fIzM2Iw8QE8VwHS9Xk7sXKeZtgdJg2%2fmH0CbPlvdUhe+eHWLROXH9ESbV5QXhyVzwK7TQtQFflg5QquS9rN8w%2fbN%2fwo6g8DXJkPsve0WewLIId8U1DheUXw4QXTBW53viX1Kbmo+WFjIZyHNuwALNf+QKZDh0wrqvGvkW%2fxe8%2fOIrMatuepXTVuTiy65B4uETdGqX5Rai0yV03oCRZH+5mr8IhE3eqVpUhv6DGNr03V1%2frhZi39KD8kJDCynOCMOWb3kiuNHOzFauoUJ3bHfO3RWCNGRWQDOHnhcPRqGgWQcMTn2XITLRtXYTuA%2fbgq4HlsHhxFTWauMy4P46dZW2M5sj5Qoa2EkVVll+NXKorQcq3H+KlSc%2fjnfUXmvykY6m8LUvx0uRZWPjvVFy04vH%2fogG1RZnY+%2fkSzJg8F8u2yEwo057CZ6%2f+DtMfX4KNSVZs735VAyryUvH1m89j5qvicGfYSv6d6c9gxptf45AV27tf1VCNi8k7sHCqY73WtRXt8dy%2fB2DFqUiU2bhXo6EmDAf2DsBDO%2f3wkwUPOP1cBCZ+2xNnFA0%2f9vFDUmccKJPpBdP5ID1LgxTpkOxI54mfck0MhBYHYoO5K1DIqTjpLqwrkNF%2fBMYN6464TqHwcXeBp+sNX5I6LapqL6G6LB9ZaSdwaPtO7EsuadzJxu4TP8U0wf0x5MEEdI8ORVgHDdxc9bdbzhM6bQ2qtVdQq38eigsLcHzfbuzbcxz51tT5%2fZkG0feOw6jBXRAR6gsPdVto2qlvHk5pqENFXT0qC%2fNw4cRe7PhyD06L10ya5h6DIQ+PRmK3a49TrXaFu%2frmE8mlumrU6m8F2dk4uWs7tu08hVJHSRamqLV4KC4fD3dKQ0A7K0PjZRdUlEZg%2f6kAvJ+pRrn0Y+sYlsVWYlZEAeL8S9C+XSVcb3qaVdDWtsOlK21QVe6NnDJv%2fJTpga3FqtsnmDbFxM%2frVJcw+c5MPNQxG34u155X3SUN6qq8cSY7BB+luuOYo783mrOwbGwZlAp36fAaFTL%2fOwhTUmw5TEqOomVt9U7kgNprajC2Yzn6eVcgwKccbm1roLmlrsPVE+Wl1qit7ID8cg8cL%2fDAFxddkG%2fjHhEiZVVj6dj96HHjyMjlSGz8vBOW873cLDFkEBERkSJa9BJWIiIiUg5DBhERESmCIYOIiIgUwZBBREREimDIICIiIkUwZBAREZEiGDKIiIhIEQwZREREpAiGDCIiIlIEQwYREREpgiGDiIiIFOGAe5cQERFRc8CeDCIiIlIEQwYREREpgiGDiIiIFMGQQURERIpgyCAiIiJFMGQQERGRIhgyiIiISBEMGURERKQIhgwiIiJSBEMGERERKYIhg4iIiBTBkEFERESKYMggIiIiRTBkEBERkSIYMoiIiEgRDBlERESkAOD%2fAbNE%2fkzV99OJAAAAAElFTkSuQmCC";
            var data = data1.Replace("%2f", "/");
            byte[] bytes1 = Convert.FromBase64String(data);
            MemoryStream memStream1 = new MemoryStream(bytes1);
            //BinaryFormatter binFormatter1 = new BinaryFormatter();
            Image a=new Bitmap(memStream1);
            //Image img = (Image)binFormatter1.Deserialize(memStream1);
            a.Save("10_3.jpg");
         }
    }
}
