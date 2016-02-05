namespace BrightBox.Services
{
    using BrightBox.Models;

    interface ITechnicalWorksRepository
    {
        TechnicalWorks GetTechnicalWorksStatus();

        bool SaveContact(TechnicalWorks technicalWorks);
    }
}
