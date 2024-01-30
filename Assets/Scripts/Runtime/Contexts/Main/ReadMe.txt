StrangeIoC:

Root sahnedeki bir objeye verilerek Context'in çalışmasını sağlar. Context kendi içinde bind işlemleri yapar.
    1- Modelleri ve model interfacelerini birbirine bağlar
    2- Eventleri commandlere bağlar
    3- View ve mediator yapılarını bağlar.
    
Mediator objeye verilmez sadece view verilir. Obje oluşturulduğunda buradaki bind işleminden dolayı mediator
objeye otomatik olarak eklenir.

Modeller verimizi tuttuğumuz yerdir. Dışarıdan sadece interface aracılığıyla ulaşabiliriz.

-------------

Eventleri tutmak için Enum klasörü içinde MainEvent bulunmaktadır. View ve Mediatorlerin kendi özel eventleri
bulunmaktadır. PanelKey, panellerin addressable'ını tutar. Panel manager'de bu address ile obje oluşturulur.

Modellerde veri işlemleri bulunmaktadır. Aynı zamanda Jsona kayıt ve jsondan yükleme işlemleri bulunmaktadır.
Orada birkaç dictionary açmamın sebebi veri çekmek istendiğinde O(1) ile veri çekilmesi. Bir miktar memory harcar fakat
oldukça hızlı çalışır. Bu projede çok veri olmayacaktır ama yine de ona göre çalışmak gerekli. Bazen de memory önceliklidir,
projenin gereksimine göre değişir.

Library Mediatorde bütün işlemleri yapmak orayı çok karmaşık yapacaktır. O yüzden panel panel ayırıp gerekli işlemleri,
kendi içinde yapmak daha verimli. Üç farklı panel yaptım, hepsi kendi işini görmekte. Book List için iki farklı item yaptım
çünkü panelde göstermek istediğim veriler arasında fark var.