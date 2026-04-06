import { Title, Text } from "@mantine/core";

export default function PageTitle({ name, description }) {
    return (
        <div>
            <Title order={2}>{name}</Title>
            <Text c="dimmed">{description}</Text>
        </div>
    )
}