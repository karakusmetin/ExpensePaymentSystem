
![Expense Payment System (1)](https://github.com/karakusmetin/ExpensePaymentSystem/assets/106442941/84fa349d-b660-432d-b5a8-90f379e91184)


Kodluyoruz ve Akbank iş birliği ile yapılan **Akbank .Net Bootcamp**'nin bitirme projesi olarak **Expense Payment System** adlı uygulamayı geliştirdim.Bu proje, sahada çalışan personelin masraf takibi ve ödeme yönetimi için bir uygulamayı içermektedir. Sistem, şirket içindeki yönetici ve saha personeli olmak üzere iki farklı rolde hizmet vermektedir.

# Masraf Ödeme Sistemi

## Kullanılan Teknolojiler
- ![JWT](https://img.shields.io/badge/JWT-black?style=for-the-badge&logo=JSON%20web%20tokens)
- ![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
- ![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Sever-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
  


## Kurulum
1. Proje bağımlılıklarını yüklemek için:
   ```bash
   git clone https://github.com/karakusmetin/ExpensePaymentSystem.git
2. Proje bağımlılıklarını yüklemek için:
   ```bash
   dotnet restore
3. Veritabanını oluşturmak için:
   ```bash
   dotnet ef database update
4. Projeyi çalıştırmak için:
   ```bash
   dotnet run

## 1. Kullanıcı İşlemleri

### Personel

- Kendi masraf girişlerini yapabilir.
- Sadece kendi masraf tanımlarını görüntüleyebilir.
- Taleplerini ilgili kriterlere göre filtreleyebilir.
- Ret olan talepler için neden ret olduklarına dair açıklama görebilir.
### Admin

- Tüm personelin taleplerini görüntüleyebilir.
- Tüm personelleri ve admin rollerini yönetebilir.
- Harcama taleplerini değerlendirip onaylayabilir veya reddedebilir.
- Ödeme kategorisi vb. sabit alanlar ayrı tablolarda tutulmuş ve sadece yönetici tarafından ekleme, çıkarma ve güncelleme işlemleri yapılabilmektedir.

## Masraf İşlemleri

- Masraf talebi için kategori bilgisi zorunlu tutulmuştur.
- Talep edilen ödeme kategorisi, ödeme aracı, ödeme yapılan konum ve fiş veya fatura vb. belgeler sisteme yüklenebilmektedir.
- Talep onaylandığında sanal bir ödeme simülasyonu gerçekleştirilerek ödeme işlemi sonlandırılmaktadır.


Şirket için tanımlı kullanıcılar sistem kurulumu ile birlikte default olarak en az 2 kullanıcı ile açılmalıdır.
#### Admin olarak giriş yapmak için gerekli olan bilgiler: 

  **username**:  admin

  **pasword**:   123456

![image](https://github.com/karakusmetin/Patika-Cohorts-Assignment7-Week5/assets/106442941/6b8af841-96d5-4e90-a347-3182f6f72d1f)

![image](https://github.com/karakusmetin/Patika-Cohorts-Assignment7-Week5/assets/106442941/acfc60f8-3c88-4f83-ae3f-41a2e0599d68)

![image](https://github.com/karakusmetin/ExpensePaymentSystem/assets/106442941/5e95212e-ebbd-45b6-901c-9ac53e3a5c50)

![image](https://github.com/karakusmetin/Patika-Cohorts-Assignment7-Week5/assets/106442941/d390d413-388e-46f3-aafb-95aca4b1571d)

![image](https://github.com/karakusmetin/Patika-Cohorts-Assignment7-Week5/assets/106442941/76d4656c-e273-4795-98a5-bdcf31e88bce)

## İletişim
  <br>
     Linkedin: <a href="https://www.linkedin.com/in/metin-karaku%C5%9F"> Linkedin Hesabım</a>
  <br>
     Mail Adresim: <a href="#"> karakusmeetin@gmail.com</a>
  

Project Link: [https://github.com/karakusmetin/ExpensePaymentSystem](https://github.com/karakusmetin/ExpensePaymentSystem)


  
<img src="https://raw.githubusercontent.com/TanZng/TanZng/master/assets/hollor_knight3.gif" width="200"/>
<p align="right">(<a href="#readme-top">back to top</a>)</p>
