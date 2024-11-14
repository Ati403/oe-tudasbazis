from transformers import AutoModel, AutoTokenizer
import torch
from app.core.config import settings

class EmbeddingService:
    def __init__(self) -> None:
        self.model = AutoModel.from_pretrained(settings.model_name)
        self.tokenizer = AutoTokenizer.from_pretrained(settings.model_name)

    def generate_embedding(self, text: str) -> list[float]:
        inputs = self.tokenizer(text, return_tensors="pt", padding=True, truncation=True)
        with torch.no_grad():
            outputs = self.model(**inputs)
            embedding = outputs.last_hidden_state.mean(dim=1).squeeze()
        return embedding.numpy().tolist()