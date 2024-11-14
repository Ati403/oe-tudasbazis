from fastapi import APIRouter, Depends
from app.models.EmbeddingRequestDto import EmbeddingRequestDto
from app.models.VectorEmbeddingDto import VectorEmbeddingDto
from app.services.embedding_service import EmbeddingService

router = APIRouter()

def get_embedding_service():
    return EmbeddingService()

@router.post("/embed", response_model=VectorEmbeddingDto)
async def embed_text(
    embedding_request: EmbeddingRequestDto,
    embedding_service: EmbeddingService = Depends(get_embedding_service)
):
    vector_embedding = embedding_service.generate_embedding(embedding_request.text)
    return VectorEmbeddingDto(vector_embedding=vector_embedding)