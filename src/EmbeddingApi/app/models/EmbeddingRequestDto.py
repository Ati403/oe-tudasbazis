from pydantic import BaseModel

class EmbeddingRequestDto(BaseModel):
    text: str