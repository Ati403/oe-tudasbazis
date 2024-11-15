from fastapi import FastAPI
from app.api.v1 import embedding_controller

app = FastAPI()

app.include_router(embedding_controller.router, prefix="/api/v1")