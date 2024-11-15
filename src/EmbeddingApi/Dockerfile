FROM python:3.13-bookworm

WORKDIR /app

COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt
RUN pip install --no-cache-dir torch --index-url https://download.pytorch.org/whl/cpu

COPY app app
COPY main.py .

EXPOSE 80
CMD ["fastapi", "run", "main.py", "--port", "80"]