from pydantic_settings import BaseSettings

class Settings(BaseSettings):
    model_name: str = "danieleff/hubert-base-cc-sentence-transformer"

    class Config:
        env_file = ".env"

settings = Settings()