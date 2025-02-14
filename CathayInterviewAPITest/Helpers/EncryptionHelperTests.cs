using CathayInterviewAPI.Helpers;

namespace CathayInterviewAPITest.Helpers
{
    public class EncryptionHelperTests
    {
        [Fact]
        public void EncryptDecrypt_ShouldReturn_OriginalString()
        {
            // Arrange
            string originalText = "Hello, Encryption!";

            // Act
            string encryptedText = EncryptionHelper.Encrypt(originalText);
            string decryptedText = EncryptionHelper.Decrypt(encryptedText);

            // Assert
            Assert.NotNull(encryptedText);
            Assert.NotEqual(originalText, encryptedText); // 加密後的內容應該不相同
            Assert.Equal(originalText, decryptedText); // 解密後應該等於原始內容
        }

        [Fact]
        public void Encrypt_ShouldReturn_DifferentValuesForDifferentInputs()
        {
            // Arrange
            string text1 = "Hello, Encryption!";
            string text2 = "Hello, World!";

            // Act
            string encrypted1 = EncryptionHelper.Encrypt(text1);
            string encrypted2 = EncryptionHelper.Encrypt(text2);

            // Assert
            Assert.NotEqual(encrypted1, encrypted2); // 不同的輸入應該產生不同的加密結果
        }

        [Fact]
        public void EncryptDecrypt_ShouldHandle_EmptyString()
        {
            // Arrange
            string emptyText = "";

            // Act
            string encryptedText = EncryptionHelper.Encrypt(emptyText);
            string decryptedText = EncryptionHelper.Decrypt(encryptedText);

            // Assert
            Assert.NotNull(encryptedText);
            Assert.Equal(emptyText, decryptedText);
        }

        [Fact]
        public void EncryptDecrypt_ShouldHandle_LongStrings()
        {
            // Arrange
            string longText = new string('A', 1000); // 1000 個 'A'

            // Act
            string encryptedText = EncryptionHelper.Encrypt(longText);
            string decryptedText = EncryptionHelper.Decrypt(encryptedText);

            // Assert
            Assert.NotNull(encryptedText);
            Assert.Equal(longText, decryptedText);
        }
    }
}
