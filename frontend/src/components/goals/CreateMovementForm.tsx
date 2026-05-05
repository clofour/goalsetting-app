import { Button, Group, Input, SegmentedControl, Stack, Textarea, TextInput } from "@mantine/core";
import { useForm, schemaResolver } from "@mantine/form";
import { postApiGoalCreateMovement } from "@/api/endpoints/goal/goal.js";
import { PostApiGoalCreateMovementBody } from "@/api/endpoints/goal/goal.zod.js";

interface CreateMovementFormProps {
    close: () => void;
    setAlert: (alert: string) => void;
    parentId: string;
}

export default function CreateMovementForm({ close, setAlert, parentId }: CreateMovementFormProps) {
    const formSchema = PostApiGoalCreateMovementBody.omit({
        bearingId: true
    })
    const form = useForm({
        mode: 'uncontrolled',
        validate: schemaResolver(formSchema, { sync: true })
    })

    const handleSubmit = async (values: typeof form.values) => {
        const requestData = {
            ...values,
            bearingId: parentId
        }
        const response = await postApiGoalCreateMovement(requestData);

        if (response.status === 200) {
            close();
        } else {
            setAlert(response.data ?? "An error has occured.");
        }
    };

    return (
        <>
            <form onSubmit={form.onSubmit(handleSubmit, (errors) => console.log(errors))}>
                <Stack>
                    <TextInput
                        label="Name"
                        description="What is your goal?"
                        placeholder="Be healthy"
                        required
                        key={form.key('name')}
                        {...form.getInputProps('name')}
                    />
                    <Textarea
                        label="Description"
                        description="What does your goal consist of?"
                        placeholder="Be healthy"
                        required
                        key={form.key('description')}
                        {...form.getInputProps('description')}
                    />
                    <Input.Wrapper
                        label="Motivation Type"
                        description="What type of motivation will you use?"
                    >
                        <SegmentedControl
                            data={[{ value: 0, label: "Carrot" }, { value: 1, label: "Stick" }]}
                            fullWidth
                            key={form.key('motivationType')}
                            {...form.getInputProps('motivationType')}
                        />
                    </Input.Wrapper>
                    <Textarea
                        label="Motivation"
                        description="How will you motivate yourself?"
                        placeholder="Be healthy"
                        key={form.key('motivation')}
                        {...form.getInputProps('motivation')}
                    />
                    <Textarea
                        label="Triggers"
                        description="How will you remind yourself to work on your goal?"
                        placeholder="Be healthy"
                        key={form.key('triggers')}
                        {...form.getInputProps('triggers')}
                    />
                    <Textarea
                        label="Temptations"
                        description="Will any temptations get in your way? How will you remove these temptations?"
                        placeholder="Be healthy"
                        key={form.key('temptations')}
                        {...form.getInputProps('temptations')}
                    />
                    <Textarea
                        label="Obstacles"
                        description="What obstacles will you face while you try to accomplish your goal? How will you face these?"
                        placeholder="Be healthy"
                        key={form.key('obstacles')}
                        {...form.getInputProps('obstacles')}
                    />
                    <Textarea
                        label="Kill Conditions"
                        description="When will you stop working on this goal?"
                        placeholder="Be healthy"
                        key={form.key('killConditions')}
                        {...form.getInputProps('killConditions')}
                    />

                    <Group justify="flex-end" mt="md">
                        <Button type="submit">Submit</Button>
                    </Group>
                </Stack>
            </form>
        </>
    )
}