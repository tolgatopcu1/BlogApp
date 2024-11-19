BlogApp
BlogApp, kullanıcıların blog yazıları yazıp yayınlayabileceği, diğer kullanıcıların ise bu bloglara yorum yapabileceği bir web uygulamasıdır. Uygulama, .NET Core MVC şablonuyla geliştirilmiş olup, basit bir kullanıcı girişi ve blog yönetim paneli sunmaktadır.

Özellikler
Kullanıcılar giriş yaparak blog yazıları oluşturabilir ve yayınlayabilir.
Kullanıcılar, bloglara yorum yapabilir.
Blog yazıları ve yorumlar veri tabanında saklanır.
Basit ve kullanıcı dostu bir arayüz.

Teknolojiler
Backend: .NET Core 7 MVC
Veritabanı: SQLite Server (Entity Framework Core)
Frontend: HTML, CSS, JavaScript (Bootstrap)

Kurulum
Projeyi yerel bilgisayarınızda çalıştırmak için aşağıdaki adımları takip edebilirsiniz:

1. Repo'yu Klonlayın
Projeyi bilgisayarınıza klonlayın:
git clone https://github.com/tolgatopcu1/BlogApp.git

2. Bağımlılıkları Yükleyin
Projeye girin ve gerekli NuGet paketlerini yükleyin:
cd BlogApp
dotnet restore

3. Veritabanını Oluşturun
Veritabanı ve gerekli tabloları oluşturmak için migrasyonları uygulayın:
dotnet ef database update

4. Uygulamayı Başlatın
Uygulamayı çalıştırın:
dotnet run

Katkı
Eğer bu projeye katkıda bulunmak isterseniz, aşağıdaki adımları takip edebilirsiniz:
Bu repoyu fork'layın.
Yeni bir özellik ekleyin veya mevcut bir hatayı düzeltin.
Değişikliklerinizi commit edin ve pull request olarak gönderin.
