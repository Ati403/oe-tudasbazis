from pydantic import BaseModel
from typing import List

class VectorEmbeddingDto(BaseModel):
    vector_embedding: List[float]